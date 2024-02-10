using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetPatientAllVisitNotificationsQueryResponse : IGetPatientAllVisitNotificationsQueryResponse
    {
        public List<VisitNotificationsDto> visitNotifications { get; set; }
    }
}
