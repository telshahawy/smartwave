using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateVisitStatusByPatientCommand
    {
        public Guid VisitId { get; set; }
        public int VisitActionTypeId { get; set; }
        public int VisitStatusTypeId { get; set; }
    }
}
