using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistPermitForEditQueryHandler : IQueryHandler<IGetChemistPermitForEditQuery, IGetChemistPermitForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistPermitForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistPermitForEditQueryResponse Read(IGetChemistPermitForEditQuery query)
        {
            IQueryable<ChemistPermitsView> dbQuery = _context.ChemistPermitsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var chemistPermit = dbQuery.SingleOrDefault(x => x.ChemistPermitId == query.ChemistPermitId && x.IsDeleted != true && x.ClientId == query.ClientId);
            if (chemistPermit == null)
            {
                throw new Exception(ErrorCodes.NotFound.ToString());
            }
            return new GetChemistPermitForEditQueryResponse()
            {
                Permit  = new ChemistPermitDto
                {
                    ChemistId = chemistPermit.ChemistId,
                    ChemistPermitId = chemistPermit.ChemistPermitId,
                    CreatedAt = chemistPermit.CreatedAt,
                    CreatedBy = chemistPermit.CreatedBy,
                    EndTime = chemistPermit.EndTime,
                    PermitDate = chemistPermit.PermitDate,
                    StartTime = chemistPermit.StartTime
                }
               
            } as IGetChemistPermitForEditQueryResponse;
        }
    }
}
