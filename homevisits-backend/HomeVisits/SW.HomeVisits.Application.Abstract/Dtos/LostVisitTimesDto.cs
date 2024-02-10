using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class LostVisitTimesDto
    {
        public Guid LostVisitTimeId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid VisitId { get; set; }
        public TimeSpan LostTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public string VisitNo { get; set; }
        public int VisitCode { get; set; }
    }
}
