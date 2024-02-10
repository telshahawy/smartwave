using Hangfire;
using Microsoft.Extensions.Options;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.BackGroundService.AssignAndOptimizeVisits
{
    public class AssignAndOptimizeVisitsRecurringJob : IAssignAndOptimizeVisitsRecurringJob
    {
        private readonly AppSettings _appSettings;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IAssignAndOptimizeVisitsJobService _assignAndOptimizeVisitsJobService;

        public AssignAndOptimizeVisitsRecurringJob(IRecurringJobManager recurringJobManager, IAssignAndOptimizeVisitsJobService assignAndOptimizeVisitsJobService, IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
            _recurringJobManager = recurringJobManager;
            _assignAndOptimizeVisitsJobService = assignAndOptimizeVisitsJobService;
        }

        //public AssignAndOptimizeVisitsRecurringJob(IOptions<AppSettings> settings)
        //{
        //    _appSettings = settings.Value;
        //}

        public void AssignAndOptimizeVisits()
        {
            //RecurringJob.AddOrUpdate<IAssignAndOptimizeVisitsJobService>(
            //assignAndOptimizeVisitsJob => assignAndOptimizeVisitsJob.AssignAndOptimizeVisitsAsync(), _appSettings.AssignAndOptimizeVisitsHangfireCronExpression);
            _recurringJobManager.AddOrUpdate("AssignAndOptimizeVisitsRecurringJob", () => _assignAndOptimizeVisitsJobService.AssignAndOptimizeVisitsAsync(), _appSettings.AssignAndOptimizeVisitsHangfireCronExpression);
        }
    }
}
