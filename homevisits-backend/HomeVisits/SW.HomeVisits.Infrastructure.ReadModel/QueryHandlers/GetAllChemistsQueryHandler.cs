using System;
using Common.Logging;
using SW.Framework.Cqrs;
using System.Linq;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using SW.HomeVisits.Domain.Enums;
using System.Globalization;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetAllChemistsQueryHandler : IQueryHandler<IGetAllChemistsQuery, IGetAllChemistsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAllChemistsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAllChemistsQueryResponse Read(IGetAllChemistsQuery query)
        {
            IQueryable<ChemistsView> dbQuery = _context.ChemistsViews;

            var chemistQuery = dbQuery.ToList().GroupBy(x => x.ChemistId);

            return new GetAllChemistsQueryResponse()
            {
                Chemists = chemistQuery.Select(x => new ChemistDto
                {
                    ChemistId = x.Key,
                    Name = x.First().Name,
                    PhoneNumber = x.First().PhoneNumber,
                    ClientId = x.First().ClientId

                }).ToList()
            } as IGetAllChemistsQueryResponse;
        }
    }
}
