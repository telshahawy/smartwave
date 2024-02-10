using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    class GetSystemParametersByClientIdForEditQueryHandler : IQueryHandler<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;
        public GetSystemParametersByClientIdForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public IGetSystemParametersForEditQueryResponse Read(IGetSystemParametersByClientIdForEditQuery query)
        {
           
            IQueryable<SystemParametersView> dbQuery = _context.SystemParametersViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var systemParameters = dbQuery.SingleOrDefault(x => x.ClientId == query.ClientId);
            if (systemParameters == null)
            {
                return new GetSystemParametersForEditQueryResponse
                {
                    SystemParameters = null

                } as IGetSystemParametersForEditQueryResponse;
            }

            return new GetSystemParametersForEditQueryResponse
            {
                SystemParameters = new SystemParametersDto
                {
                    ClientId = systemParameters.ClientId,
                    CreateBy = systemParameters.CreateBy,
                    DefaultCountryId = systemParameters?.DefaultCountryId,
                    DefaultGovernorateId = systemParameters?.DefaultGovernorateId,
                    EstimatedVisitDurationInMin = systemParameters.EstimatedVisitDurationInMin,
                    NextReserveHomevisitInDay = systemParameters.NextReserveHomevisitInDay,
                    OptimizezonebeforeInMin = systemParameters?.OptimizezonebeforeInMin,
                    RoutingSlotDurationInMin = systemParameters.RoutingSlotDurationInMin,
                    VisitApprovalBy = systemParameters.VisitApprovalBy,
                    VisitCancelBy = systemParameters.VisitCancelBy,
                    WhatsappBusinessLink = systemParameters?.WhatsappBusinessLink,
                    PrecautionsFile = systemParameters?.PrecautionsFile,
                    CallCenterNumber = systemParameters?.CallCenterNumber,
                    IsOptimizezonebefore = systemParameters?.IsOptimizezonebefore,
                    IsSendPatientTimeConfirmation = systemParameters?.IsSendPatientTimeConfirmation,
                    FileName=systemParameters?.FileName

                }

            } as IGetSystemParametersForEditQueryResponse;
        }
    }
}
