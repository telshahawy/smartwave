using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Collections.Generic;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetAllVisitNotificationsQueryResponse : IGetAllVisitNotificationsQueryResponse
    {
        public List<VisitNotificationsDto> visitNotifications { get; set; }
    }
}
