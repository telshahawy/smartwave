using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistVisitsScheduleQuery : IGetChemistVisitsScheduleQuery
    {
        public Guid ChemistId { get; set; }

        public DateTime date { get; set; }

        public CultureNames CultureName { get; set; }
    }
}
