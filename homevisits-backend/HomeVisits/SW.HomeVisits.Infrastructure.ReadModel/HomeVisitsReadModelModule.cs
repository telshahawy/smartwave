using SW.Framework.Cqrs;
using SW.Framework.Decorators;
using SW.Framework.DependencyInjection;
using SW.Framework.Security;
using SW.Framework.Validation;

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using SW.HomeVisits.Application.Configuration;
using System;

namespace SW.HomeVisits.Infrastructure.ReadModel
{
    public class HomeVisitsReadModelModule
    {
        public void Initialise(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<HomeVisitsReadModelContext>(options =>
                 options.UseSqlServer(DataBaseConnectionString.GetDataConnectionStringFromConfig(), builder =>
                 {
                     builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                 }));

            serviceCollection.AddTransient<IQueryHandler<IGetUserQuery, IIsUserExist>, GetUserQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchPatientScheduleQuery, ISearchPatientScheduleQueryResponse>, SearchPatientScheduleQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetPatientAllVisitNotificationsQuery, IGetPatientAllVisitNotificationsQueryResponse>, GetPatientAllVisitNotificationsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetGeoZoneForEditQuery, IGetTimeZonesForGeoZoneQueryResponse>, GetTimeZonesForGeoZoneQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptCancelPermissionQueryResponse>, GetVisitAcceptCancelPermissionQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetSystemParametersByClientIdForEditQuery, IGetVisitDatesRegardingSysParamQueryResponse>, GetVisitDatesRegardingSysParamQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptAndCancelPermissionQueryResponse>, GetVisitAcceptAndCancelPermissionQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetPendingVisitsListHomePageQuery, ISearchVisitsQueryResponse>, GetPendingVisitsListHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetReassignedVisitsListHomePageQuery, ISearchVisitsQueryResponse>, GetReassignedVisitsListHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetDelayedVisitsListHomePageQuery, ISearchVisitsQueryResponse>, GetDelayedVisitsListHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitHomePageQuery, ISearchVisitsQueryResponse>, GetVisitsListHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetRejectedVisitReportQuery, IGetRejectedVisitReportQueryResponse>, GetRejectedVisitReportQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetCanceledVisitReportQuery, IGetCanceledVisitReportQueryResponse>, GetCanceledVisitReportQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitReportQuery, IGetVisitReportQueryResponse>, GetVisitReportQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitReportQuery, IGetNonDetailedVisitReportQueryResponse>, GetNonDetailedVisitReportQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitsHomePageQuery, IGetVisitsHomePageQueryResponse>, GetVisitsHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetIdleChemistHomePageQuery, IGetIdleChemistHomePageQueryResponse>, GetIdleChemistHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetUserAreasForHomePageQuery, IGetUserAreasForHomePageQueryResponse>, GetUserAreasForHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAllChemistHomePageQuery, IGetAllChemistHomePageQueryResponse>, GetAllChemistHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetActiveChemistHomePageQuery, IGetActiveChemistHomePageQueryResponse>, GetActiveChemistHomePageQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>, GetSystemParametersByClientIdForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetCityQuery, IGetCityQueryResponse>, GetCityQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchMyScheduleQuery, ISearchMyScheduleQueryResponse>, SearchMyScheduleQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchChemistsQuery, ISearchChemistsQueryResponse>, SearchChemistsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAllVisitNotificationsQuery, IGetAllVisitNotificationsQueryResponse>, GetAllVisitNotificationsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetCountriesKeyValueQuery, IGetCountriesKeyValueQueryResponse>, GetCountriesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetGovernatsKeyValueQuery, IGetGovernatsKeyValueQueryResponse>, GetGovernatsKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetGeoZonesKeyValueQuery, IGetGeoZonesKeyValueQueryResponse>, GetGeoZonesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchPatientsQuery, ISearchPatientsQueryResponse>, SearchPatientsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetPatientVisitsByPatientIdQuery, IGetPatientVisitsByPatientIdQueryResponse>, GetPatientVisitsByPatientIdQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>, GetVisitDetailsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>, GetAvailableVisitsInAreaQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistGeoZonesKeyValueQuery, IGetChemistGeoZonesKeyValueQueryResponse>, GetChemistGeoZonesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAllLostVisitTimesQuery, IGetAllLostVisitTimesQueryResponse>, GetAllLostVisitTimesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetReasonsKeyValueQuery, IGetReasonsKeyValueQueryResponse>, GetReasonsKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>, GetUserDeviceByChemistIdQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAllAgeSegmentsQuery, IGetAllAgeSegmentsQueryResponse>, GetAllAgeSegmentsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>, GetChemistSchedulePlanQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistVisitsScheduleQuery, IGetChemistVisitsScheduleQueryResponse>, GetChemistVisitsScheduleQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistTrackingLogQuery, IGetChemistTrackingLogQueryResponse>, GetChemistTrackingLogQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAllChemistsQuery, IGetAllChemistsQueryResponse>, GetAllChemistsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchVisitsQuery, ISearchVisitsQueryResponse>, SearchVisitsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistsKeyValueQuery, IGetChemistsKeyValueQueryResponse>, GetChemistsKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistForEditQuery, IGetChemistForEditQueryResponse>, GetChemistForEditQueryQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchRolesQuery, ISearchRolesQueryResponse>, SearchRolesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetRoleForEditQuery, IGetRoleForEditQueryResponse>, GetRoleForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchClientUserQuery, ISearchClientUserQueryResponse>, SearchClientUserQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetClientUserForEditQuery, IGetClientUserForEditQueryResponse>, GetClientUserForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetRolesKeyValueQuery, IGetRolesKeyValueQueryResponse>, GetRolesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetSystemPagesWithPermissionsTreeQuery, IGetSystemPagesWithPermissionsTreeQueryResponse>, GetSystemPagesWithPermissionsTreeQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetSystemPageKeyValueQuery, IGetSystemPagesKeyValueQueryResponse>, GetSystemPagesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetClientByNameQuery, IGetClientByNameQueryResponse>, GetClientByNameQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>, GetChemistsByTimeZoneIdQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>, GetPatientByPatientIdQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchChemistScheduleQuery, ISearchChemistScheduleQueryResponse>, SearchChemistScheduleQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistAssignedGeoZoneKeyValueQuery, IGetChemistAssignedGeoZoneKeyValueQueryResponse>, GetChemistAssignedGeoZonesKeyValueQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchReasonsQuery, ISearchReasonsQueryResponse>, SearchReasonsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetReasonForEditQuery, IGetReasonForEditQueryResponse>, GetReasonForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IPatientsListQuery, IPatientsListQueryResponse>, PatientsListQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetPatientForEditQuery, IGetPatientForEditQueryResponse>, GetPatientForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistRoutesQuery, IGetChemistRoutesQueryResponse>, GetChemistRoutesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistPermitForEditQuery, IGetChemistPermitForEditQueryResponse>, GetChemistPermitForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchChemistPermitsQuery, ISearchChemistPermitsQueryResponse>, SearchChemistPermitsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchCountriesQuery, ISearchCountriesQueryResponse>, SearchCountriesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetCountryForEditQuery, IGetCountryForEditQueryResponse>, GetCountryForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchGovernatsQuery, ISearchGovernatsQueryResponse>, SearchGovernatsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetGovernateForEditQuery, IGetGovernateForEditQueryResponse>, GetGovernateForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchGeoZonesQuery, ISearchGeoZonesQueryResponse>, SearchGeoZonesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetGeoZoneForEditQuery, IGetGeoZoneForEditQueryResponse>, GetGeoZoneForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchAgeSegmentsQuery, ISearchAgeSegmentsQueryResponse>, SearchAgeSegmentsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAgeSegmentForEditQuery, IGetAgeSegmentForEditQueryResponse>, GetAgeSegmentForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistScheduleForEditQuery, IGetChemistScheduleForEditQueryResponse>, GetChemistScheduleForEditQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetAvailableVisitsForChemistQuery, IGetAvailableVisitsInAreaQueryResponse>, GetAvailableVisitsForChemistQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>, SearchChemistVisitsOrderQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetTimeZoneFramesQuery, IGetTimeZoneFramesQueryResponse>, GetTimeZoneFramesQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetSystemPagePermissionQuery, IGetSystemPagePermissionQueryResponse>, GetSystemPermissionQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetUserPermissionQuery, IGetUserPermissionQueryResponse>, GetUserPermissionQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistVisitInPermitTimeQuery, IGetChemistVisitInPermitTimeQueryResponse>, GetChemistVisitInPermitTimeQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetChemistsLastTrackingsQuery, IGetChemistsLastTrackingsQueryResponse>, GetChemistsLastTrackingsQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<IGetUserSystemPageWithPermissionsTreeQuery, IGetUserSystemPageWithPermissionsTreeQueryResponse>, GetUserSystemPageWithPermissionsTreeQueryHandler>();

            #region Register Security Rules

            //containerBuilder.RegisterDefaultAuthorizationManager();
            //serviceCollection.AddTransient<IAuthorisationManager, AuthorisationManager>();


            //Add all query handlers that you want to secure with Auth Manager
            //serviceCollection.AddTransient<IQueryHandler<IGetUserRoles, IGetUserRolesResponse>, AuthorisationQueryHandlerDecorator<IGetUserRoles, IGetUserRolesResponse>>();//<IGetUserRoles, IGetUserRolesResponse>();                                                                                                                                                             //container.RegisterAll<IAuthorisationRule<IGetUserRoles>>((new[] { typeof(SessionAuthRule) }));
            #endregion


            #region Register Validation Rules

            //serviceCollection.AddTransient<IValidationManager, ValidationManager>();

            //Add all query handlers that you want to secure with Auth Manager
            //serviceCollection.AddTransient<IQueryHandler<IGetUserRoles, IGetUserRolesResponse>, ValidationQueryHandlerDecorator<IGetUserRoles, IGetUserRolesResponse>>();                                                                                                                                                      //container.RegisterAll<IAuthorisationRule<IGetUserRoles>>((new[] { typeof(SessionAuthRule) }));
            //containerBuilder.RegisterAll<IValidationRule<IGetUserRoles>>((new[]
            //{
            //    typeof(CheckNullableMessageValidationRule),
            //    typeof(CheckPermissionsValidationRule)
            //}));

            #endregion

            //Add all query handlers that you want to decorate with logging
            //serviceCollection.AddTransient<IQueryHandler<IGetUserRoles, IGetUserRolesResponse>, QueryLoggingDecorator<IGetUserRoles, IGetUserRolesResponse>>();

        }
    }
}