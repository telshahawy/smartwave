using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetClientByNameQueryHandler : IQueryHandler<IGetClientByNameQuery, IGetClientByNameQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetClientByNameQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetClientByNameQueryResponse Read(IGetClientByNameQuery query)
        {
            IQueryable<ClientView> dbQuery = _context.ClientViews;
            ClientView client = new ClientView();
            if(query != null)
            {
                 client = dbQuery.SingleOrDefault(x => x.ClientName.ToLower() == query.Name.ToLower());
            }
            if(client == null)
            {
                throw new Exception("client not found");
            }
            return new GetClientQueryResponse()
            {
                Client = new ClientDto
                {
                    ClientName = client.ClientName,
                    ClientCode = client.ClientCode,
                    ClientId = client.ClientId,
                    CountryId = client.CountryId,
                    DisplayName = client.DisplayName,
                    IsActive = client.IsActive,
                    IsDeleted = client.IsDeleted,
                    Logo = client.Logo,
                    URLName = client.URLName
                }
            };
        }
    }
}

