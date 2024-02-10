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
    internal class GetAllVisitNotificationsQueryHandler : IQueryHandler<IGetAllVisitNotificationsQuery, IGetAllVisitNotificationsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAllVisitNotificationsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAllVisitNotificationsQueryResponse Read(IGetAllVisitNotificationsQuery query)
        {
            IQueryable<VisitsNotificationsView> dbQuery = _context.VisitsNotificationsViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(n => n.ChemistId == query.ChemistId).OrderByDescending(n => n.CreationDate).Take(25);
            }

            return new GetAllVisitNotificationsQueryResponse()
            {
                visitNotifications = dbQuery.Select(n => new VisitNotificationsDto
                {
                    VisitId = n.VisitId,
                    NotificationId = n.NotificationId,
                    Title = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? n.TitleAr : n.Title,
                    Message = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? n.MessageAr : n.Message,
                    CreationDate = n.CreationDate
                }).ToList()
            } as IGetAllVisitNotificationsQueryResponse;
        }
    }
}
