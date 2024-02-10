using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateLostVisitTimeCommand
    {
        public Guid LostVisitTimeId { get; set; }
        public Guid ChemistId { get; set; }
        public Guid VisitId { get; set; }
        public string LostTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
