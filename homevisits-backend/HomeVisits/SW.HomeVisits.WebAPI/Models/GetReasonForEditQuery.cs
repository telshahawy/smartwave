using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetReasonForEditQuery : IGetReasonForEditQuery
    {
        public int ReasonId { get; set; }
    }
}
