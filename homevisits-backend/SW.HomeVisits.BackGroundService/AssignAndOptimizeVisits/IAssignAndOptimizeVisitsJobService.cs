using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SW.HomeVisits.BackGroundService.AssignAndOptimizeVisits
{
    public interface IAssignAndOptimizeVisitsJobService
    {
        Task AssignAndOptimizeVisitsAsync();
    }
}
