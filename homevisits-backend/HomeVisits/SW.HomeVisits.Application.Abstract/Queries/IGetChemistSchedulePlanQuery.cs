using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistSchedulePlanQuery
    {
        public Guid ClientId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime date { get; set; }
        public bool DayFilter { get; set; }

    }
}
