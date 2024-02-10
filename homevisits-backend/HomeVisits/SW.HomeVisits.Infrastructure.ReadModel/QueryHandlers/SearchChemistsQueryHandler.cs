using System;
using System.Data.Entity;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class SearchChemistsQueryHandler : IQueryHandler<ISearchChemistsQuery, ISearchChemistsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchChemistsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchChemistsQueryResponse Read(ISearchChemistsQuery query)
        {
            IQueryable<ChemistsView> dbQuery = _context.ChemistsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId && (query.CountryId == null || x.CountryId == query.CountryId) &&
                (query.GovernateId == null || x.GovernateId == query.GovernateId) &&
                (query.GovernateId == null || x.GovernateId == query.GovernateId) &&
                (query.GeoZoneId == null || x.GeoZoneId == query.GeoZoneId) &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.ChemistName) || x.Name == query.ChemistName) &&
                (query.Gender == null || x.Gender == query.Gender) &&
                  (string.IsNullOrWhiteSpace(query.PhoneNo) || x.PhoneNumber == query.PhoneNo) &&
                  (query.ChemistStatus == null || x.IsActive == query.ChemistStatus) &&
                  (query.ExpertChemist == null || x.ExpertChemist == query.ExpertChemist) &&
                (query.JoinDateFrom == null || x.JoinDate.Date >= query.JoinDateFrom.Value.Date) &&
                (query.JoinDateto == null || x.JoinDate.Date <= query.JoinDateto.Value.Date)
                );
           
                var chemistQuery = dbQuery.OrderBy(x => x.Code).ToList().GroupBy(x => x.ChemistId);
                chemistQuery = chemistQuery.Where(x => (query.AreaAssignStatus == null || x.Where(x=>x.AssignedGeoZoneIsDeleted != true).Select(g=>g.GeoZoneId).Any(x=>x != null) == query.AreaAssignStatus));
                 var totalCount = chemistQuery.Count();
                if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
                {
                    int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                    chemistQuery = chemistQuery.Skip(skipRows).Take(query.PageSize.Value);
                }
   
            return new SearchChemistsQueryResponse()
            {
                Chemists = chemistQuery.Select(x => new ChemistDto
                {
                    ChemistId = x.Key,
                    Age = DateTime.Now.Year - x.First().BirthDate.GetValueOrDefault().Year,
                    Code = x.First().Code,
                    //CountryId = x.Concat< x.CountryId,
                    CountryName = query.cultureName == CultureNames.ar ? string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).GroupBy(x => x.CountryNameAr).Select(i => i.Key)) : string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).GroupBy(x => x.CountryNameEn).Select(i => i.Key)),
                    Gender = x.First().Gender,
                    GenderName = query.cultureName == CultureNames.ar ? x.First().Gender == 1 ? "ذكر" : "انثى" : x.First().Gender == 1 ? "Male" : "Female",
                    //GeoZoneId = x.GeoZoneId,
                    GeoZoneName = query.cultureName == CultureNames.ar ? string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).Select(i => i.GeoZoneNameAr)) : string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).Select(i => i.GeoZoneNameEn)),
                    //GovenateId = x.GovernateId,
                    GovenateName = query.cultureName == CultureNames.ar ? string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).GroupBy(x => x.GoverNameAr).Select(i => i.Key)) : string.Join(",", x.Where(x => x.AssignedGeoZoneIsDeleted != true).GroupBy(x => x.GoverNameEn).Select(i => i.Key)),
                    IsActive = x.First().IsActive,
                    JoinDate = x.First().JoinDate,
                    Name = x.First().Name,
                    PhoneNumber = x.First().PhoneNumber,
                    ClientId = x.First().ClientId
                }).OrderBy(x => x.Code).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchChemistsQueryResponse;
        }
    }
}
