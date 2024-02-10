using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SecondVisitTimeZoneVisitDateDto
    {
        public Guid OriginVisitId { get; set; }
        public int MinMinutes { get; set; }
        public int MaxMinutes { get; set; }
        public DateTime SecondVisitDate { get; set; }
        public Guid SecondVisitTimeFrameId { get; set; }
    }
}
