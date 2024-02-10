using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class CreateVisitStatusByPatientCommand : ICreateVisitStatusByPatientCommand
    {
        public Guid VisitId { get; set; }
        public int VisitActionTypeId { get; set; }
        public int VisitStatusTypeId { get; set; }
    }
}
