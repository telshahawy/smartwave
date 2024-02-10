using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistForEditQuery : IGetChemistForEditQuery
    {
        public Guid ChemistId { get; set; }
    }
}
