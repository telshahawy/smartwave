using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract;
using Microsoft.Extensions.Options;
using SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits;

namespace SW.HomeVisits.BackGroundService.AssignAndOptimizeVisits
{
    public class AssignAndOptimizeVisitsJobService : IAssignAndOptimizeVisitsJobService
    {
        private readonly IAssignAndOptimizeVisitsService _assignAndOptimizeVisitsService;

        public AssignAndOptimizeVisitsJobService(IAssignAndOptimizeVisitsService assignAndOptimizeVisitsService)
        {
            _assignAndOptimizeVisitsService = assignAndOptimizeVisitsService;
        }

        public async Task AssignAndOptimizeVisitsAsync()
        {
            try
            {
                await _assignAndOptimizeVisitsService.AssignAndOptimizeVisitsInRecurringJobAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
