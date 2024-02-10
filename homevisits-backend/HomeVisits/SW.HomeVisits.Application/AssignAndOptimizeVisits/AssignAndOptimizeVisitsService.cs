using Microsoft.Extensions.Options;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SW.HomeVisits.Application.AssignAndOptimizeVisits
{
    public class AssignAndOptimizeVisitsService : IAssignAndOptimizeVisitsService
    {
        #region Fields
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly AppSettings _appSettings;
        #endregion

        #region Constructors
        public AssignAndOptimizeVisitsService(ICommandBus commandBus, IQueryProcessor queryProcessor, IOptions<AppSettings> settings)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _appSettings = settings.Value;
        }
        #endregion

        public async Task AssignAndOptimizeVisitsInAddVisitsAsync(VisitDetailsDto addedVisit, Guid timeZoneGeoZoneId, DateTime visitDate, Guid clientId)
        {
            if (await IsAddVisitAfterTimeZoneStartAsync(addedVisit, visitDate))
            {
                AddChemistVisitOrderCommand chemistVisitOrderToTargetChemist;

                var chemistsAvailabeOnTimeZoneGeoZone = await _queryProcessor.ProcessQueryAsync<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>(new GetChemistsByTimeZoneIdQuery
                {
                    ClientId = clientId,
                    TimeZoneGeoZoneId = timeZoneGeoZoneId,
                    date = visitDate
                });
                if (chemistsAvailabeOnTimeZoneGeoZone == null || chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists == null || chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count == 0)
                    return;


                if (!addedVisit.ChemistId.HasValue)
                {
                    AvailableChemistsDto targetChemist = null;

                    if (chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count == 1)
                    {
                        targetChemist = chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.FirstOrDefault();
                        chemistVisitOrderToTargetChemist = await GetChemistVisitOrderToTargetChemistAsync(addedVisit,
                            chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists, timeZoneGeoZoneId, clientId, visitDate,
                            targetChemist.ChemistId);
                    }
                    else
                    {
                        var systemParameters = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                        {
                            ClientId = clientId
                        });
                        if (systemParameters == null || systemParameters.SystemParameters == null)
                            return;

                        var visitsByTimeZoneGeoZoneId = await _queryProcessor.ProcessQueryAsync<ISearchVisitsQuery, ISearchVisitsQueryResponse>(new SeachVisitsQuery
                        {
                            ClientId = clientId,
                            TimeZoneGeoZoneId = timeZoneGeoZoneId,
                            VisitDate = visitDate
                        });

                        var visitsInTimeZone = visitsByTimeZoneGeoZoneId.Visits.Where(x => x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled && x.VisitStatusTypeId != (int)VisitStatusTypes.Reject).ToList();
                        List<AvailableChemistsDto> chemistAvialableToTakeExtraVisitsInTimeZone = new List<AvailableChemistsDto>();
                        foreach (var availableChemist in chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists)
                        {
                            if (IsChemistAvialableToTakeExtraVisitsInTimeZone(availableChemist, visitDate, visitsInTimeZone, systemParameters.SystemParameters))
                                chemistAvialableToTakeExtraVisitsInTimeZone.Add(availableChemist);
                        }

                        if (chemistAvialableToTakeExtraVisitsInTimeZone.Count == 1)
                        {
                            targetChemist = chemistAvialableToTakeExtraVisitsInTimeZone.FirstOrDefault();
                            chemistVisitOrderToTargetChemist = await GetChemistVisitOrderToTargetChemistAsync(addedVisit,
                               chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists, timeZoneGeoZoneId, clientId, visitDate,
                               targetChemist.ChemistId);
                        }
                        else
                        {
                            if (chemistAvialableToTakeExtraVisitsInTimeZone.Count == 0)
                            {
                                targetChemist = chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.FirstOrDefault();
                                chemistVisitOrderToTargetChemist = await GetChemistVisitOrderToTargetChemistAsync(addedVisit,
                                   chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists, timeZoneGeoZoneId, clientId, visitDate,
                                   targetChemist.ChemistId);
                            }
                            else
                            {
                                int visitOrderCurrentVisit = 0;
                                float startLatitudeCurrentVisit = 0, startLongitudeCurrentVisit = 0;
                                ChemistDestinationRouteDto chemistDestinationRouteDtoCurrentVisit = null;
                                chemistVisitOrderToTargetChemist = new AddChemistVisitOrderCommand
                                {
                                    TimeZoneFrameId = timeZoneGeoZoneId,
                                    VisitId = addedVisit.VisitId,
                                    Latitude = addedVisit.Latitude,
                                    Longitude = addedVisit.Longitude
                                };

                                int minDurationInTraffic = int.MaxValue;
                                foreach (var item in chemistAvialableToTakeExtraVisitsInTimeZone)
                                {
                                    var chemistVisitsOrderInTimeZone = await _queryProcessor.ProcessQueryAsync<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>(new SearchChemistVisitsOrderQuery
                                    {
                                        TimeZoneFrameId = timeZoneGeoZoneId,
                                        ClientId = clientId,
                                        VisitDate = visitDate,
                                        ChemistId = item.ChemistId,
                                        IsDeleted = false
                                    });
                                    int visitOrder = 0;
                                    float startLatitude = 0, startLongitude = 0;
                                    if (chemistVisitsOrderInTimeZone != null && chemistVisitsOrderInTimeZone.Visits != null && chemistVisitsOrderInTimeZone.Visits.Count > 0)
                                    {
                                        var lastChemistVisitInTimeZone = chemistVisitsOrderInTimeZone.Visits.OrderByDescending(p => p.VisitOrder).FirstOrDefault();
                                        visitOrder = lastChemistVisitInTimeZone.VisitOrder + 1;
                                        startLatitude = float.Parse(lastChemistVisitInTimeZone.Latitude);
                                        startLongitude = float.Parse(lastChemistVisitInTimeZone.Longitude);
                                    }
                                    else
                                    {
                                        visitOrder = 1;
                                        startLatitude = item.StartLatitude;
                                        startLongitude = item.StartLangitude;
                                    }

                                    var chemistVisitsDestination = GetChemistDestinationRoute(item.ChemistId, startLongitude.ToString(), startLatitude.ToString(), addedVisit);
                                    if (chemistVisitsDestination.DurationInTraffic < minDurationInTraffic)
                                    {
                                        targetChemist = item;
                                        visitOrderCurrentVisit = visitOrder;
                                        startLatitudeCurrentVisit = startLatitude;
                                        startLongitudeCurrentVisit = startLongitude;
                                        minDurationInTraffic = chemistVisitsDestination.DurationInTraffic;
                                        chemistDestinationRouteDtoCurrentVisit = chemistVisitsDestination;
                                    }
                                }
                                chemistVisitOrderToTargetChemist.ChemistId = targetChemist.ChemistId;
                                chemistVisitOrderToTargetChemist.StartLangitude = startLongitudeCurrentVisit;
                                chemistVisitOrderToTargetChemist.StartLatitude = startLatitudeCurrentVisit;
                                chemistVisitOrderToTargetChemist.VisitOrder = visitOrderCurrentVisit;
                                chemistVisitOrderToTargetChemist.Distance = chemistDestinationRouteDtoCurrentVisit.Distance;
                                chemistVisitOrderToTargetChemist.Duration = chemistDestinationRouteDtoCurrentVisit.Duration;
                                chemistVisitOrderToTargetChemist.DurationInTraffic = chemistDestinationRouteDtoCurrentVisit.DurationInTraffic;
                            }
                        }
                    }

                    addedVisit.ChemistId = targetChemist.ChemistId;
                    await AssignChemistAsync(addedVisit.VisitId, targetChemist.ChemistId, timeZoneGeoZoneId, visitDate);
                }
                else
                {
                    chemistVisitOrderToTargetChemist = await GetChemistVisitOrderToTargetChemistAsync(addedVisit,
                        chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists, timeZoneGeoZoneId, clientId, visitDate,
                        addedVisit.ChemistId.Value);
                }

                await AddChemistVisitOrderAsync(chemistVisitOrderToTargetChemist);
            }
            else
            {
                var visitsByTimeZoneGeoZoneId = await _queryProcessor.ProcessQueryAsync<ISearchVisitsQuery, ISearchVisitsQueryResponse>(new SeachVisitsQuery
                {
                    ClientId = clientId,
                    TimeZoneGeoZoneId = timeZoneGeoZoneId,
                    VisitDate = visitDate
                });

                if (visitsByTimeZoneGeoZoneId != null && visitsByTimeZoneGeoZoneId.Visits != null && visitsByTimeZoneGeoZoneId.Visits.Count > 0)
                {
                    var chemistsAvailabeOnTimeZoneGeoZone = await _queryProcessor.ProcessQueryAsync<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>(new GetChemistsByTimeZoneIdQuery
                    {
                        ClientId = clientId,
                        TimeZoneGeoZoneId = timeZoneGeoZoneId,
                        date = visitDate
                    });
                    if (visitsByTimeZoneGeoZoneId.Visits.Count >= visitsByTimeZoneGeoZoneId.Visits.FirstOrDefault().VisitsNoQouta * chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count)
                        await AssignAndOptimizeVisitsAsync(visitsByTimeZoneGeoZoneId.Visits);
                }
            }
        }

        private async Task<AddChemistVisitOrderCommand> GetChemistVisitOrderToTargetChemistAsync(VisitDetailsDto addedVisit, List<AvailableChemistsDto> availableChemists, Guid timeZoneGeoZoneId, Guid clientId, DateTime visitDate, Guid chemistId)
        {
            AddChemistVisitOrderCommand addChemistVisitOrderCommand = new AddChemistVisitOrderCommand
            {
                ChemistId = chemistId,
                TimeZoneFrameId = timeZoneGeoZoneId,
                VisitId = addedVisit.VisitId,
                Latitude = addedVisit.Latitude,
                Longitude = addedVisit.Longitude
            };

            var chemistVisitsOrderInTimeZone = await _queryProcessor.ProcessQueryAsync<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>(new SearchChemistVisitsOrderQuery
            {
                TimeZoneFrameId = timeZoneGeoZoneId,
                ClientId = clientId,
                VisitDate = visitDate,
                ChemistId = addedVisit.ChemistId,
                IsDeleted = false
            });

            if (chemistVisitsOrderInTimeZone != null && chemistVisitsOrderInTimeZone.Visits != null && chemistVisitsOrderInTimeZone.Visits.Count > 0)
            {
                var lastChemistVisitInTimeZone = chemistVisitsOrderInTimeZone.Visits.OrderByDescending(p => p.VisitOrder).FirstOrDefault();
                addChemistVisitOrderCommand.VisitOrder = lastChemistVisitInTimeZone.VisitOrder + 1;
                addChemistVisitOrderCommand.StartLatitude = float.Parse(lastChemistVisitInTimeZone.Latitude);
                addChemistVisitOrderCommand.StartLangitude = float.Parse(lastChemistVisitInTimeZone.Longitude);
            }
            else
            {
                var chemistDto = availableChemists.FirstOrDefault(p => p.ChemistId == chemistId);
                addChemistVisitOrderCommand.VisitOrder = 1;
                addChemistVisitOrderCommand.StartLatitude = chemistDto.StartLatitude;
                addChemistVisitOrderCommand.StartLangitude = chemistDto.StartLangitude;
            }
            var chemistDestinationRouteDtoCurrentVisit = GetChemistDestinationRoute(chemistId, addChemistVisitOrderCommand.StartLangitude.ToString(), addChemistVisitOrderCommand.StartLatitude.ToString(), addedVisit);
            addChemistVisitOrderCommand.Distance = chemistDestinationRouteDtoCurrentVisit.Distance;
            addChemistVisitOrderCommand.Duration = chemistDestinationRouteDtoCurrentVisit.Duration;
            addChemistVisitOrderCommand.DurationInTraffic = chemistDestinationRouteDtoCurrentVisit.DurationInTraffic;
            return addChemistVisitOrderCommand;
        }

        private bool IsChemistAvialableToTakeExtraVisitsInTimeZone(AvailableChemistsDto chemistTimeZoneAvailabilityView, DateTime visitDate, List<VisitsDto> allVisitsView, SystemParametersDto systemParameter)
        {
            bool result = false;
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Now.Date;
            var currentDateTime = DateTime.Now;

            var chemistVisitsInTimeZone = allVisitsView.Where(p => p.ChemistId.HasValue && p.ChemistId.Value == chemistTimeZoneAvailabilityView.ChemistId).ToList();

            if (chemistVisitsInTimeZone != null && chemistVisitsInTimeZone.Count > 0)
            {
                TimeSpan timeZoneStartTime = chemistVisitsInTimeZone.FirstOrDefault().TimeZoneStartTime;
                TimeSpan timeZoneEndTime = chemistVisitsInTimeZone.FirstOrDefault().TimeZoneEndTime;

                var chemistVisitsCreatedBeforeTimeZoneStart = chemistVisitsInTimeZone.Where(p => p.CreatedDate.TimeOfDay <= timeZoneStartTime).ToList();
                if (chemistVisitsCreatedBeforeTimeZoneStart.Count <= chemistVisitsCreatedBeforeTimeZoneStart.Where(p => p.VisitStatusTypeId == (int)VisitStatusTypes.Done).Count())
                {
                    var chemistAvailableTimeInMinutes = GetChemistAvailableTimeInMinutes(timeZoneStartTime, timeZoneEndTime, chemistTimeZoneAvailabilityView.ChemistStartTime, chemistTimeZoneAvailabilityView.ChemistEndTime, visitDate);
                    if (chemistAvailableTimeInMinutes >= systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    {
                        var numOfAdditionalVisitAvialable = chemistAvailableTimeInMinutes / (systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin);
                        var chemistVisitCreatedAfterTimeZoneStart = chemistVisitsInTimeZone.Where(p => p.CreatedDate.TimeOfDay > timeZoneStartTime).ToList();
                        if (chemistVisitCreatedAfterTimeZoneStart.Count < numOfAdditionalVisitAvialable)
                            result = true;
                    }
                }
            }
            else
                result = true;
            return result;
        }

        private int GetChemistAvailableTimeInMinutes(TimeSpan timezoneStart, TimeSpan timezoneEnd, TimeSpan chemistStartTime, TimeSpan chemistEndTime, DateTime queryDate)
        {
            var availableMinues = 0;
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Now.Date;

            if ((chemistStartTime >= timezoneStart && chemistStartTime <= timezoneEnd) &&
                (chemistEndTime >= timezoneStart && chemistEndTime <= timezoneEnd))
            {
                TimeSpan span = currentDate == queryDate && currentTime > chemistStartTime ? chemistEndTime - currentTime : chemistEndTime - chemistStartTime;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistStartTime >= timezoneStart && chemistStartTime <= timezoneEnd && chemistEndTime >= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > chemistStartTime ? timezoneEnd - currentTime : timezoneEnd - chemistStartTime;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistStartTime <= timezoneStart && chemistEndTime >= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > timezoneStart ? timezoneEnd - currentTime : timezoneEnd - timezoneStart;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistEndTime >= timezoneStart && chemistEndTime <= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > timezoneStart ? chemistEndTime - currentTime : chemistEndTime - timezoneStart;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            return availableMinues;
        }

        public async Task AssignAndOptimizeVisitsInRecurringJobAsync()
        {
            try
            {
                var visitDate = DateTime.Now;
                var allVisitsInDay = await _queryProcessor.ProcessQueryAsync<ISearchVisitsQuery, ISearchVisitsQueryResponse>(new SeachVisitsQuery
                {
                    VisitDate = visitDate,
                    TimeZoneStartTime = visitDate.TimeOfDay
                });
                if (allVisitsInDay == null || allVisitsInDay.Visits == null || allVisitsInDay.Visits.Count == 0)
                    return;

                var visitsByTimeZoneFrame = allVisitsInDay.Visits.Where(x => x.VisitStatusTypeId != (int)VisitStatusTypes.Done && x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled
                && x.VisitStatusTypeId != (int)VisitStatusTypes.Reject).ToList().GroupBy(p => p.TimeZoneFrameId);

                foreach (var visits in visitsByTimeZoneFrame)
                {
                    if (visits.All(x => x.ChemistId.HasValue))
                        continue;

                    var systemParameters = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                    {
                        ClientId = visits.FirstOrDefault().ClientId
                    });
                    if (systemParameters == null || systemParameters.SystemParameters == null)
                        continue;

                    TimeSpan timeAfterAddMinutes = systemParameters.SystemParameters.OptimizezonebeforeInMin.HasValue ? DateTime.Now.AddMinutes(systemParameters.SystemParameters.OptimizezonebeforeInMin.Value).TimeOfDay : visitDate.TimeOfDay;
                    if (visits.FirstOrDefault().TimeZoneStartTime >= visitDate.TimeOfDay && visits.FirstOrDefault().TimeZoneStartTime <= timeAfterAddMinutes)
                    {
                        await AssignAndOptimizeVisitsAsync(visits.ToList(), true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task AssignAndOptimizeVisitsAsync(List<VisitsDto> visits, bool isRecurringJob = false)
        {
            if (visits == null || visits == null || visits.Count == 0)
                return;

            var systemParameterResponse = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
            {
                ClientId = visits.FirstOrDefault().ClientId
            });
            if (systemParameterResponse == null || systemParameterResponse.SystemParameters == null)
                return;

            var systemParameters = systemParameterResponse.SystemParameters;
            DateTime visitDate = visits.FirstOrDefault().VisitDateValue;
            visits = visits.Where(x => x.VisitStatusTypeId != (int)VisitStatusTypes.Done && x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled
                && x.VisitStatusTypeId != (int)VisitStatusTypes.Reject).ToList();
            var chemistsAvailabeOnTimeZoneGeoZone = await _queryProcessor.ProcessQueryAsync<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>(new GetChemistsByTimeZoneIdQuery
            {
                ClientId = visits.FirstOrDefault().ClientId,
                TimeZoneGeoZoneId = visits.FirstOrDefault().TimeZoneFrameId,
                date = visitDate
            });

            if (chemistsAvailabeOnTimeZoneGeoZone == null || chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists == null || chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count == 0)
                return;

            var visitsNotAssignOnChemists = visits.Where(p => p.ChemistId == null).ToList();
            var availabeChemists = chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Where(p => p.TotalVisits < p.VisitsNoQuota).ToList();
            if (availabeChemists.Count == 0)
                return;
            else
            {
                List<ChemistDestinationRouteDto> chemistVisitsDestinations = new List<ChemistDestinationRouteDto>();
                var geoZoneForEditQueryResponse = await _queryProcessor.ProcessQueryAsync<IGetGeoZoneForEditQuery, IGetGeoZoneForEditQueryResponse>(new GetGeoZoneForEditQuery
                {
                    GeoZoneId = visits.FirstOrDefault().GeoZoneId,
                });

                var timeZoneResult = geoZoneForEditQueryResponse.GeoZone.TimeZoneFrames.ToList();
                TimeZoneFramesDto prevTimeZoneFrame = null;
                var currentTimeZoneIndex = timeZoneResult.FindIndex(p => p.TimeZoneFrameId == visits.FirstOrDefault().TimeZoneFrameId);
                if (currentTimeZoneIndex > 0)
                    prevTimeZoneFrame = timeZoneResult[currentTimeZoneIndex - 1];

                List<ChemistVisitsOrderDto> prevTimeZoneFrameVisits = null;
                if (prevTimeZoneFrame != null && !prevTimeZoneFrame.BranchDispatch)
                {
                    var prevVisitsInTimeZone = await _queryProcessor.ProcessQueryAsync<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>(new SearchChemistVisitsOrderQuery
                    {
                        TimeZoneFrameId = prevTimeZoneFrame.TimeZoneFrameId,
                        ClientId = visits.FirstOrDefault().ClientId,
                        VisitDate = visitDate,
                        IsDeleted = false
                    });
                    if (prevVisitsInTimeZone != null && prevVisitsInTimeZone.Visits != null && prevVisitsInTimeZone.Visits.Count > 0)
                    {
                        if (isRecurringJob || (!isRecurringJob && prevVisitsInTimeZone.Visits.Count >= prevTimeZoneFrame.VisitsNoQuota * chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count))
                        {
                            prevTimeZoneFrameVisits = prevVisitsInTimeZone.Visits;
                        }
                        else
                            return;
                    }
                    else
                    {
                        if (!isRecurringJob)
                            return;
                    }
                }

                foreach (var chemist in availabeChemists)
                {
                    float startLangitude, startLatitude;
                    GetStartPosition(chemist, prevTimeZoneFrameVisits, out startLatitude, out startLangitude);
                    chemistVisitsDestinations.AddRange(GetChemistDestinationRoute(chemist.ChemistId, startLangitude, startLatitude, visitsNotAssignOnChemists));
                }
                var chemistVisitDestinationsHaveTime = chemistVisitsDestinations.Where(p => p.VisitTime.HasValue).OrderBy(p => p.VisitTime).ToList();
                var chemistVisitDestinationsWithoutTime = chemistVisitsDestinations.Where(p => !p.VisitTime.HasValue).OrderBy(p => p.DurationInTraffic).ToList();
                chemistVisitDestinationsHaveTime.AddRange(chemistVisitDestinationsWithoutTime);
                chemistVisitsDestinations = chemistVisitDestinationsHaveTime;

                decimal numberOfVisitForChemist = Convert.ToDecimal(visits.Count) / Convert.ToDecimal(chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists.Count);
                decimal maxNumberOfVisitForChemist = Math.Ceiling(numberOfVisitForChemist);
                //int maxDifferentInVisitNumberBetweenChemists = 2; //this mean chemist at maximum take double others
                foreach (var chemistVisitsDestination in chemistVisitsDestinations)
                {
                    var targetVisit = visitsNotAssignOnChemists.FirstOrDefault(p => p.VisitId == chemistVisitsDestination.VisitId);
                    if (targetVisit.ChemistId.HasValue)
                        continue;

                    var targetChemist = availabeChemists.FirstOrDefault(p => p.ChemistId == chemistVisitsDestination.ChemistId);
                    if (targetChemist.TotalVisits >= targetChemist.VisitsNoQuota || targetChemist.TotalVisits >= maxNumberOfVisitForChemist) //availabeChemists.Min(p => p.TotalVisits) * maxDifferentInVisitNumberBetweenChemists)
                        continue;

                    if (targetChemist.ChemistVisits != null && targetChemist.ChemistVisits.Count > 0 && targetVisit.VisitTime.HasValue && targetChemist.ChemistVisits.Any(p => p.VisitTime.HasValue))
                    {
                        if (targetChemist.ChemistVisits.Any(p => p.VisitTime.HasValue && p.VisitTime.Value >= targetVisit.VisitTime.Value && p.VisitTime.Value <= targetVisit.VisitTime.Value.Add(new TimeSpan(0, systemParameters.EstimatedVisitDurationInMin, 0))))
                            continue;
                        if (targetChemist.ChemistVisits.Any(p => p.VisitTime.HasValue && p.VisitTime.Value <= targetVisit.VisitTime.Value && p.VisitTime.Value.Add(new TimeSpan(0, systemParameters.EstimatedVisitDurationInMin, 0)) >= targetVisit.VisitTime.Value))
                            continue;
                    }

                    targetVisit.ChemistId = targetChemist.ChemistId;
                    targetChemist.ChemistVisits.Add(new VisitsDto { VisitId = targetVisit.VisitId, VisitTime = targetVisit.VisitTime });
                    targetChemist.TotalVisits += 1;

                    await AssignChemistAsync(targetVisit.VisitId, targetChemist.ChemistId, targetVisit.TimeZoneFrameId, targetVisit.VisitDateValue);
                }

                foreach (var chemistVisitsDestination in chemistVisitsDestinations)
                {
                    var targetVisit = visitsNotAssignOnChemists.FirstOrDefault(p => p.VisitId == chemistVisitsDestination.VisitId);
                    if (targetVisit.ChemistId.HasValue)
                        continue;

                    var targetChemist = availabeChemists.FirstOrDefault(p => p.ChemistId == chemistVisitsDestination.ChemistId);
                    if (targetChemist.TotalVisits >= targetChemist.VisitsNoQuota || targetChemist.TotalVisits >= maxNumberOfVisitForChemist)
                        continue;

                    targetVisit.ChemistId = targetChemist.ChemistId;
                    targetChemist.ChemistVisits.Add(new VisitsDto { VisitId = targetVisit.VisitId, VisitTime = targetVisit.VisitTime });
                    targetChemist.TotalVisits += 1;

                    await AssignChemistAsync(targetVisit.VisitId, targetChemist.ChemistId, targetVisit.TimeZoneFrameId, targetVisit.VisitDateValue);
                }

                // sort chemist visits after assign operation compeleted.
                var chemistsVisitsInTimeZoneAfterAssigned = await _queryProcessor.ProcessQueryAsync<ISearchVisitsQuery, ISearchVisitsQueryResponse>(new SeachVisitsQuery
                {
                    TimeZoneGeoZoneId = visits.FirstOrDefault().TimeZoneFrameId,
                    ClientId = visits.FirstOrDefault().ClientId,
                    VisitDate = visitDate
                });
                foreach (var availableChemist in chemistsAvailabeOnTimeZoneGeoZone.AvailableChemists)
                {
                    float startLangitude, startLatitude;
                    GetStartPosition(availableChemist, prevTimeZoneFrameVisits, out startLatitude, out startLangitude);

                    var chemistVisitsInTimeZoneAfterAssigned = chemistsVisitsInTimeZoneAfterAssigned.Visits.Where(p => p.ChemistId == availableChemist.ChemistId).ToList();

                    var chemistDestinationRoute = GetChemistDestinationRoute(availableChemist.ChemistId, startLangitude, startLatitude, chemistVisitsInTimeZoneAfterAssigned);
                    var chemistVisitHaveTime = chemistDestinationRoute.Where(p => p.VisitTime.HasValue).OrderBy(p => p.VisitTime).ToList();
                    var chemistVisitWithoutTime = chemistDestinationRoute.Where(p => !p.VisitTime.HasValue).OrderBy(p => p.DurationInTraffic).ToList();
                    //chemistVisitHaveTime.AddRange(chemistVisitWithoutTime);
                    //chemistDestinationRoute = chemistVisitHaveTime;
                    List<ChemistDestinationRouteDto> chemistVisitOrderList = new List<ChemistDestinationRouteDto>();
                    if (chemistDestinationRoute.Any(p => p.VisitTime.HasValue))
                    {
                        var currentTimeZone = timeZoneResult[currentTimeZoneIndex];
                        for (int i = 0; i < chemistVisitHaveTime.Count; i++)
                        {
                            if ((chemistVisitHaveTime[i].VisitTime.Value - currentTimeZone.StartTimeValue).TotalMinutes <= systemParameters.EstimatedVisitDurationInMin)
                                chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                            else
                            {
                                if (i == 0)
                                {
                                    var minutesBetweenVisitAndStartTime = (chemistVisitHaveTime[i].VisitTime.Value - currentTimeZone.StartTimeValue).TotalMinutes;
                                    var numVisit = (int)Math.Ceiling(minutesBetweenVisitAndStartTime / systemParameters.EstimatedVisitDurationInMin);
                                    if (numVisit == 0 || numVisit == 1)
                                        chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                                    else
                                    {
                                        if (chemistVisitWithoutTime != null && chemistVisitWithoutTime.Count > 0 && chemistVisitWithoutTime.Any(p => !chemistVisitOrderList.Any(m => m.VisitId == p.VisitId)))
                                        {
                                            for (int j = 1; j < numVisit; j++)
                                            {
                                                chemistVisitOrderList.Add(chemistVisitWithoutTime.FirstOrDefault(p => !chemistVisitOrderList.Any(m => m.VisitId == p.VisitId)));
                                            }
                                        }
                                        chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                                    }
                                }
                                else
                                {
                                    var minutesBetweenVisits = (chemistVisitHaveTime[i].VisitTime.Value - chemistVisitHaveTime[i - 1].VisitTime.Value).TotalMinutes;
                                    if (minutesBetweenVisits <= systemParameters.EstimatedVisitDurationInMin)
                                        chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                                    else
                                    {
                                        var numVisit = (int)Math.Ceiling(minutesBetweenVisits / systemParameters.EstimatedVisitDurationInMin);
                                        if (numVisit == 0 || numVisit == 1)
                                            chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                                        else
                                        {
                                            if (chemistVisitWithoutTime != null && chemistVisitWithoutTime.Count > 0 && chemistVisitWithoutTime.Any(p => !chemistVisitOrderList.Any(m => m.VisitId == p.VisitId)))
                                            {
                                                for (int j = 1; j < numVisit; j++)
                                                {
                                                    chemistVisitOrderList.Add(chemistVisitWithoutTime.FirstOrDefault(p => !chemistVisitOrderList.Any(m => m.VisitId == p.VisitId)));
                                                }
                                            }
                                            chemistVisitOrderList.Add(chemistVisitHaveTime[i]);
                                        }
                                    }
                                }
                            }
                        }
                        //ChemistDestinationRouteDto[] chemistVisitOrderArray = new ChemistDestinationRouteDto[visits.FirstOrDefault().VisitsNoQouta];
                        //foreach (var item in chemistDestinationRoute)
                        //{
                        //    if (item.VisitTime.HasValue)
                        //    {
                        //        var minutesBetweenVisitAndStartTime = (item.VisitTime.Value - currentTimeZone.StartTimeValue).TotalMinutes;
                        //        if (minutesBetweenVisitAndStartTime < systemParameters.EstimatedVisitDurationInMin)
                        //        {
                        //            if (chemistVisitOrderArray[0] == null)
                        //                chemistVisitOrderArray[0] = item;
                        //            else
                        //                SetVisitInCorrectOrder(chemistVisitOrderArray, 0, item);
                        //        }
                        //        else
                        //        {
                        //            var visitIndex = Math.Ceiling(minutesBetweenVisitAndStartTime / (systemParameters.EstimatedVisitDurationInMin + systemParameters.RoutingSlotDurationInMin));
                        //            if (visitIndex > chemistVisitOrderArray.Length - 1)
                        //                SetVisitInCorrectOrder(chemistVisitOrderArray, chemistVisitOrderArray.Length - 1, item);
                        //            else
                        //                SetVisitInCorrectOrder(chemistVisitOrderArray, (int)visitIndex, item); //chemistVisitOrderArray[(int)visitIndex] = item;
                        //        }
                        //    }
                        //}

                        //foreach (var item in chemistDestinationRoute)
                        //{
                        //    if (!chemistVisitOrderArray.Any(p => p != null && p.VisitId == item.VisitId))
                        //    {
                        //        for (int i = 0; i < chemistVisitOrderArray.Length; i++)
                        //        {
                        //            if (chemistVisitOrderArray[i] == null)
                        //            {
                        //                chemistVisitOrderArray[i] = chemistDestinationRoute[i];
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}

                        //foreach (var item in chemistVisitOrderArray)
                        //{
                        //    if (item != null)
                        //        chemistVisitOrderList.Add(item);
                        //}
                    }
                    else
                        chemistVisitOrderList = chemistDestinationRoute;

                    int visitOrder = 1;
                    foreach (var visitDestinations in chemistVisitOrderList)
                    {
                        VisitsDto chemistVisitAfterAssigned = chemistVisitsInTimeZoneAfterAssigned.FirstOrDefault(p => p.VisitId == visitDestinations.VisitId);
                        var addChemistVisitOrderCommand = new AddChemistVisitOrderCommand
                        {
                            VisitId = chemistVisitAfterAssigned.VisitId,
                            ChemistId = availableChemist.ChemistId,
                            Latitude = float.Parse(!string.IsNullOrEmpty(chemistVisitAfterAssigned.Latitude) ? chemistVisitAfterAssigned.Latitude : "0"),
                            Longitude = float.Parse(!string.IsNullOrEmpty(chemistVisitAfterAssigned.Longitude) ? chemistVisitAfterAssigned.Longitude : "0"),
                            StartLangitude = startLangitude,
                            StartLatitude = startLatitude,
                            TimeZoneFrameId = chemistVisitAfterAssigned.TimeZoneFrameId,
                            VisitOrder = visitOrder,
                            Distance = visitDestinations.Distance,
                            Duration = visitDestinations.Duration,
                            DurationInTraffic = visitDestinations.DurationInTraffic,
                            IsDeleted = false
                        };
                        await AddChemistVisitOrderAsync(addChemistVisitOrderCommand);
                        visitOrder += 1;
                    }
                }
            }
        }

        private void SetVisitInCorrectOrder(ChemistDestinationRouteDto[] chemistVisitOrderArray, int visitOrderIndex, ChemistDestinationRouteDto chemistDestinationRouteDto)
        {
            if (visitOrderIndex < chemistVisitOrderArray.Length && chemistVisitOrderArray[visitOrderIndex] == null)
                chemistVisitOrderArray[visitOrderIndex] = chemistDestinationRouteDto;
            else
            {
                ChemistDestinationRouteDto tempChemistDestinationRoute;
                for (int i = visitOrderIndex; i < chemistVisitOrderArray.Length; i++)
                {
                    if (chemistVisitOrderArray[i] == null)
                    {
                        chemistVisitOrderArray[i] = chemistDestinationRouteDto;
                        return;
                    }
                    else
                    {
                        if (chemistDestinationRouteDto.VisitTime > chemistVisitOrderArray[i].VisitTime)
                            continue;
                        else
                        {
                            tempChemistDestinationRoute = chemistVisitOrderArray[i];
                            chemistVisitOrderArray[i] = chemistDestinationRouteDto;
                            SetVisitInCorrectOrder(chemistVisitOrderArray, i + 1, tempChemistDestinationRoute);
                        }
                    }
                }

                for (int i = visitOrderIndex; i > 0; i--)
                {
                    if (chemistVisitOrderArray[i] == null)
                    {
                        chemistVisitOrderArray[i] = chemistDestinationRouteDto;
                        return;
                    }
                    else
                    {
                        if (chemistDestinationRouteDto.VisitTime < chemistVisitOrderArray[i].VisitTime)
                            continue;
                        else
                        {
                            tempChemistDestinationRoute = chemistVisitOrderArray[i];
                            chemistVisitOrderArray[i] = chemistDestinationRouteDto;
                            SetVisitInCorrectOrder(chemistVisitOrderArray, i - 1, tempChemistDestinationRoute);
                        }
                    }
                }
            }
        }

        private async Task<bool> IsAddVisitAfterTimeZoneStartAsync(VisitDetailsDto addedVisit, DateTime visitDate)
        {
            var currentDate = DateTime.Now.Date;
            var currentTime = DateTime.Now.TimeOfDay;

            if (currentDate == visitDate.Date)
            {
                var geoZoneForEditQueryResponse = await _queryProcessor.ProcessQueryAsync<IGetGeoZoneForEditQuery, IGetTimeZonesForGeoZoneQueryResponse>(new GetGeoZoneForEditQuery
                {
                    GeoZoneId = addedVisit.GeoZoneId,
                });
                var timeZoneResult = geoZoneForEditQueryResponse.TimeZonesForGeoZone.ToList();
                var targetTimeZoneStartTime = timeZoneResult.FirstOrDefault(p => p.TimeZoneFrameId == addedVisit.TimeZoneGeoZoneId);
                if (currentTime >= targetTimeZoneStartTime.StartTime && currentTime < targetTimeZoneStartTime.EndTime)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private async Task AssignChemistAsync(Guid visitId, Guid chemistId, Guid timeZoneGeoZoneId, DateTime visitDate)
        {
            var assignChemistCommand = new CreateVisitStatusCommand
            {
                VisitId = visitId,
                ChemistId = chemistId,
                VisitActionTypeId = (int)VisitActionTypes.ReassignChemist,
                TimeZoneGeoZoneId = timeZoneGeoZoneId,
                VisitDate = visitDate,
            };
            await _commandBus.SendAsync((ICreateVisitStatusCommand)assignChemistCommand);
        }

        private async Task AddChemistVisitOrderAsync(AddChemistVisitOrderCommand chemistVisitOrderToTargetChemist)
        {
            var addChemistVisitOrderCommand = new AddChemistVisitOrderCommand
            {
                VisitId = chemistVisitOrderToTargetChemist.VisitId,
                ChemistId = chemistVisitOrderToTargetChemist.ChemistId,
                Latitude = chemistVisitOrderToTargetChemist.Latitude,
                Longitude = chemistVisitOrderToTargetChemist.Longitude,
                StartLangitude = chemistVisitOrderToTargetChemist.StartLangitude,
                StartLatitude = chemistVisitOrderToTargetChemist.StartLatitude,
                TimeZoneFrameId = chemistVisitOrderToTargetChemist.TimeZoneFrameId,
                VisitOrder = chemistVisitOrderToTargetChemist.VisitOrder,
                Distance = chemistVisitOrderToTargetChemist.Distance,
                Duration = chemistVisitOrderToTargetChemist.Duration,
                DurationInTraffic = chemistVisitOrderToTargetChemist.DurationInTraffic,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };
            await _commandBus.SendAsync((IAddChemistVisitOrderCommand)addChemistVisitOrderCommand);
        }

        private void GetStartPosition(AvailableChemistsDto chemist, List<ChemistVisitsOrderDto> prevTimeZoneFrameVisits, out float startLatitude, out float startLangitude)
        {
            startLatitude = chemist.StartLatitude;
            startLangitude = chemist.StartLangitude;

            if (prevTimeZoneFrameVisits != null)
            {
                //var prevVisitSorted = SortVisites(chemist.ChemistId, chemist.StartLangitude, chemist.StartLatitude, prevTimeZoneFrameVisits.Where(p => p.ChemistId == chemist.ChemistId).ToList());
                var lastChemistVistitInPrevTimeZoneFrame = prevTimeZoneFrameVisits.Where(p => p.ChemistId == chemist.ChemistId).OrderByDescending(p => p.VisitOrder).FirstOrDefault();
                startLatitude = float.Parse(lastChemistVistitInPrevTimeZoneFrame.Latitude);
                startLangitude = float.Parse(lastChemistVistitInPrevTimeZoneFrame.Longitude);
            }
        }

        private GMapRoutingDto GetDistanceMatrix(string origins, List<string> destinations, DateTime departureTime)
        {
            string googleMapAPIKey = _appSettings.GoogleMapAPIKey; // passing API key
            //string googleMapClientID = _appSettings.GoogleMapClientID; // passing client id
            string travelMode = "driving"; //Driving, Walking, Bicycling, Transit.
            string urlRequest = @"https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origins + "&destinations=" + destinations.Aggregate((i, j) => i + "|" + j) + "&units=imperial" + "&departure_time=" + departureTime.Ticks + "&traffic_model=optimistic" + "&mode='" + travelMode + "'&sensor=false";
            if (!string.IsNullOrEmpty(googleMapAPIKey))
            {
                //urlRequest += "&client=" + googleMapClientID;
                urlRequest += "&key=" + googleMapAPIKey; //Sign(urlRequest, googleMapAPIKey); // request with api key and client id
            }
            WebRequest request = WebRequest.Create(urlRequest);
            request.Method = "Get";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string resp = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Deserialize<GMapRoutingDto>(resp, serializeOptions);
            return result;
        }

        private List<VisitsDto> SortVisits(Guid chemistId, float startLangitude, float startLatitude, List<VisitsDto> visitsDtos)
        {
            var chemistDestinationRoute = GetChemistDestinationRoute(chemistId, startLangitude, startLatitude, visitsDtos);
            List<VisitsDto> result = new List<VisitsDto>();
            foreach (var visitDestinations in chemistDestinationRoute)
            {
                result.Add(visitsDtos.FirstOrDefault(p => p.VisitId == visitDestinations.VisitId));
            }
            return result;
        }

        private List<ChemistDestinationRouteDto> GetChemistDestinationRoute(Guid chemistId, float startLangitude, float startLatitude, List<VisitsDto> visitsDtos)
        {
            var source = $"{startLatitude},{startLangitude}";
            var visitDate = visitsDtos.FirstOrDefault().VisitDateValue;
            var timeZoneStartTime = visitsDtos.FirstOrDefault().TimeZoneStartTime;
            DateTime departureTime = new DateTime(visitDate.Year, visitDate.Month, visitDate.Day, timeZoneStartTime.Hours, timeZoneStartTime.Minutes, timeZoneStartTime.Seconds);
            var map = GetDistanceMatrix(source, visitsDtos.Select(x => (x.Latitude + "," + x.Longitude)).ToList(), departureTime);
            var routes = new GetChemistRoutesQueryResponse()
            {
                Route = new ChemistRouteDto
                {
                    StartLatitiude = startLatitude,
                    StartLongitude = startLangitude,
                    Destinations = new List<ChemistDestinationRouteDto>()
                }
            };
            for (int i = 0; i < visitsDtos.Count(); i++)
            {
                routes.Route.Destinations.Add(new ChemistDestinationRouteDto
                {
                    ChemistId = chemistId,
                    VisitId = visitsDtos[i].VisitId,
                    Latitiude = float.Parse(!string.IsNullOrEmpty(visitsDtos[i].Latitude) ? visitsDtos[i].Latitude : "0"),
                    Longitude = float.Parse(!string.IsNullOrEmpty(visitsDtos[i].Longitude) ? visitsDtos[i].Longitude : "0"),
                    Distance = map.rows.First().elements[i].distance?.value ?? 0,
                    DurationInTraffic = map.rows.First().elements[i].duration_in_traffic?.value ?? 0,
                    Duration = map.rows.First().elements.FirstOrDefault().duration?.value ?? 0,
                    VisitTime = visitsDtos[i].VisitTime
                });
            }
            routes.Route.Destinations = routes.Route.Destinations.OrderBy(x => x.DurationInTraffic).ToList();
            return routes.Route.Destinations;
        }

        private ChemistDestinationRouteDto GetChemistDestinationRoute(Guid chemistId, string startLangitude, string startLatitude, VisitDetailsDto visitsDtos)
        {
            var source = $"{startLatitude},{startLangitude}";
            var visitDate = visitsDtos.VisitDate;
            var timeZoneStartTime = visitsDtos.TimeZoneStartTime;
            DateTime departureTime = new DateTime(visitDate.Year, visitDate.Month, visitDate.Day, timeZoneStartTime.Hours, timeZoneStartTime.Minutes, timeZoneStartTime.Seconds);
            var map = GetDistanceMatrix(source, new List<string> { visitsDtos.Latitude + "," + visitsDtos.Longitude }, departureTime);

            return new ChemistDestinationRouteDto
            {
                ChemistId = chemistId,
                Latitiude = visitsDtos.Latitude,
                Longitude = visitsDtos.Longitude,
                VisitId = visitsDtos.VisitId,
                Distance = map.rows.First().elements.FirstOrDefault().distance?.value ?? 0,
                DurationInTraffic = map.rows.First().elements.FirstOrDefault().duration_in_traffic?.value ?? 0,
                Duration = map.rows.First().elements.FirstOrDefault().duration?.value ?? 0
            };
        }

    }
}
