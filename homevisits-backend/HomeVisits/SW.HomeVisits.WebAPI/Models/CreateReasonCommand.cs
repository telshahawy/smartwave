using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateReasonCommand : ICreateReasonCommand
    {
        public int ReasonId { get; set; }
        public Guid ClientId { get; set; }
        public string ReasonName { get; set; }
        public bool IsActive { get; set; }
        public int? ReasonActionId { get; set; }
        public int VisitTypeActionId { get; set; }
    }
}
