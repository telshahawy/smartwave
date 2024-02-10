using System;
using System.Collections.Generic;
using System.Text;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistVisitsScheduleQuery
    {
        public Guid ChemistId { get; }
        public DateTime date { get; }
        public CultureNames CultureName { get; }

    }
}
