using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SecondVisitAfterFirstVisitTimeModel
    {
        public Guid OriginVisitId { get; set; }
        public int MinMinutes { get; set; }
        public int MaxMinutes { get; set; }
    }
}
