using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchPatientScheduleModel
    {
        public DateTime? VisitDate { get; set; }

        public Guid? PatientId { get; set; }

        public int? VisitStatusTypeId { get; set; }

        public Guid ClientId { get; set; }

        public CultureNames cultureName { get; set; }
        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
