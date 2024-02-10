using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetClientUserForEditQueryHandler : IQueryHandler<IGetClientUserForEditQuery, IGetClientUserForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetClientUserForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetClientUserForEditQueryResponse Read(IGetClientUserForEditQuery query)
        {
            IQueryable<UserView> dbQuery = _context.UserViews;
            IQueryable<UserGeoZonesView> userGeoZonesDbQuery = _context.UserGeoZonesViews.Where(x => x.UserId == query.UserId);
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var user = dbQuery.SingleOrDefault(x =>
            x.UserId == query.UserId &&
            x.ClientId == query.ClientId &&
            x.IsDeleted != true &&
            x.UserType == (int)UserTypes.ClientAdmin);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return new GetClientUserForEditQueryResponse
            {
                User = new ClientUserDto
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId.GetValueOrDefault(),
                    ClientId = user.ClientId,
                    Code = user.Code,
                    IsActive = user.IsActive,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    GeoZonesIds = userGeoZonesDbQuery.Where(x=>x.IsDeleted != true).Select(x => x.GeoZoneId).ToList()
                }

            } as IGetClientUserForEditQueryResponse;
        }
    }
}

