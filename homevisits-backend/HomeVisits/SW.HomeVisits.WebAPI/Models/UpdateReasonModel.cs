using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateReasonModel
    {
        public string ReasonName { get; set; }
        public bool IsActive { get; set; }
        public int? ReasonActionId { get; set; }
        public int VisitTypeActionId { get; set; }
    }
}
