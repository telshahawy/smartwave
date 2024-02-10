using SW.Framework.Cqrs;
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
    public class GetVisitDatesRegardingSysParamQueryHandler : IQueryHandler<IGetSystemParametersByClientIdForEditQuery, IGetVisitDatesRegardingSysParamQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetVisitDatesRegardingSysParamQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }

        public IGetVisitDatesRegardingSysParamQueryResponse Read(IGetSystemParametersByClientIdForEditQuery query)
        {
            IQueryable<SystemParametersView> dbQuery = _context.SystemParametersViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var systemParameters = dbQuery.SingleOrDefault(x => x.ClientId == query.ClientId);
            if (systemParameters == null)
            {
                return null;
            }
            var sysReservedDate = dbQuery.Select(x => x.NextReserveHomevisitInDay).FirstOrDefault();
            var LastDay = DateTime.Today.AddDays(sysReservedDate).Date;
            var FirstDay = DateTime.Today.Date;

            return new GetVisitDatesRegardingSysParamQueryResponse
            {
                StartDate= FirstDay.Date,
                EndDate= LastDay.Date

            } as IGetVisitDatesRegardingSysParamQueryResponse;
        }
    }
}
