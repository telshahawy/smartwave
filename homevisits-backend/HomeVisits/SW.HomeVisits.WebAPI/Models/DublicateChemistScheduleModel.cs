using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class DublicateChemistScheduleModel
    {
        public Guid ChemistScheduleId { get; set; }
        public Guid ChemistId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
