using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    class GetSystemParametersForEditQueryHandler : IQueryHandler<IGetSystemParametersForEditQuery, IGetSystemParametersForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;
        public GetSystemParametersForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public IGetSystemParametersForEditQueryResponse Read(IGetSystemParametersForEditQuery query)
        {
            throw new NullReferenceException(nameof(query));//Temporary

            //IQueryable<SystemParametersView> dbQuery = _context.SystemParametersViews;
            //if (query == null)
            //{
            //    throw new NullReferenceException(nameof(query));
            //}

            //var systemParameters = dbQuery.SingleOrDefault(x => x.SystemParametersId == query.SystemParametersId);
            //if (systemParameters == null)
            //{
            //    throw new Exception("system Parameters not found");
            //}
            //return new GetSystemParametersForEditQueryResponse
            //{
            //    SystemParameters = new SystemParametersDto
            //    {
            //        SystemParametersId = systemParameters.SystemParametersId,
            //        ClientId = systemParameters.ClientId,
            //        CreateBy = systemParameters.CreateBy,
            //        DefaultCountryId = systemParameters.DefaultCountryId,
            //        DefaultGovernorateId = systemParameters.DefaultGovernorateId,
            //        EstimatedVisitDurationInMin = systemParameters.EstimatedVisitDurationInMin,
            //        NextReserveHomevisitInDay = systemParameters.NextReserveHomevisitInDay,
            //        OptimizezonebeforeInMin = systemParameters.OptimizezonebeforeInMin,
            //        RoutingSlotDurationInMin = systemParameters.RoutingSlotDurationInMin,
            //        VisitApprovalBy = systemParameters.VisitApprovalBy,
            //        VisitCancelBy = systemParameters.VisitCancelBy,
            //        WhatsappBusinessLink = systemParameters.WhatsappBusinessLink,
            //        PrecautionsFile = systemParameters.PrecautionsFile,
            //        CallCenterNumber = systemParameters.CallCenterNumber,
            //        IsOptimizezonebefore = systemParameters.IsOptimizezonebefore,
            //        IsSendPatientTimeConfirmation = systemParameters.IsSendPatientTimeConfirmation

            //    }

            //} as IGetSystemParametersForEditQueryResponse;
        }
    }
}
