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
   public class GetPatientAllVisitNotificationsQueryHandler : IQueryHandler<IGetPatientAllVisitNotificationsQuery, IGetPatientAllVisitNotificationsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;
        public GetPatientAllVisitNotificationsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetPatientAllVisitNotificationsQueryResponse Read(IGetPatientAllVisitNotificationsQuery query)
        {
            IQueryable<VisitsNotificationsPatientView> dbQuery = _context.visitsNotificationsPatientViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(n => n.PatientId == query.PatientId).OrderByDescending(n => n.CreationDate).Take(25);
            }

            return new GetPatientAllVisitNotificationsQueryResponse()
            {
                visitNotifications = dbQuery.Select(n => new VisitNotificationsDto
                {
                    VisitId = n.VisitId,
                    NotificationId = n.NotificationId,
                    Title = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? n.TitleAr : n.Title,
                    Message = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? n.MessageAr : n.Message,
                    CreationDate = n.CreationDate
                }).ToList()
            } as IGetPatientAllVisitNotificationsQueryResponse;
        }
    }
}
