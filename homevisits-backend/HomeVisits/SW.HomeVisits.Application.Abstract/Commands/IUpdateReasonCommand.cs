using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateReasonCommand
    {
        int ReasonId { get; set; }
        string ReasonName { get; set; }
        bool IsActive { get; set; }
        int? ReasonActionId { get; set; }
        int VisitTypeActionId { get; set; }
    }
}
