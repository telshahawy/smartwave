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
    internal class GetUserDeviceByChemistIdQueryHandler : IQueryHandler<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetUserDeviceByChemistIdQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetUserDeviceByChemistIdQueryResponse Read(IGetUserDeviceByChemistIdQuery query)
        {
            IQueryable<UserDevicesView> dbQuery = _context.UserDevicesViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(u => u.UserId == query.ChemistId).OrderByDescending(u => u.CreatedAt);
            }
            var result = dbQuery.ToList();
            if (result == null || result.Count == 0)
                return new GetUserDeviceByChemistIdQueryResponse();
            return new GetUserDeviceByChemistIdQueryResponse()
            {
                UserDevice = dbQuery.Select(u => new UserDevicesDto
                {
                    UserDeviceId = u.UserDeviceId,
                    UserId = u.UserId,
                    DeviceSerialNumber = u.DeviceSerialNumber,
                    FireBaseDeviceToken = u.FireBaseDeviceToken,
                    CreatedAt = u.CreatedAt
                }).FirstOrDefault()
            } as IGetUserDeviceByChemistIdQueryResponse;
        }
    }
}
