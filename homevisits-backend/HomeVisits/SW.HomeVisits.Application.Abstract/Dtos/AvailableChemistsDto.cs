using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class AvailableChemistsDto
    {
        public Guid ChemistId { get; set; }
        public float StartLatitude { get; set; }
        public float StartLangitude { get; set; }
        public bool ExpertChemist { get; set; }
        public int VisitsNoQuota { get; set; }
        public List<VisitsDto> ChemistVisits { get; set; }
        public int TotalVisits { get; set; }
        public TimeSpan ChemistStartTime { get; set; }
        public TimeSpan ChemistEndTime { get; set; }
    }
}
