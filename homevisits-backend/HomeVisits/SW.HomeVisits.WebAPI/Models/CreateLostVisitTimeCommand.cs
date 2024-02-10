using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateLostVisitTimeCommand : ICreateLostVisitTimeCommand
    {
        public Guid LostVisitTimeId { get; set; }
        public Guid ChemistId { get; set; }
        public Guid VisitId { get; set; }
        public string LostTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
