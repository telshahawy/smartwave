using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetVisitsHomePageQueryResponse : IGetVisitsHomePageQueryResponse
    {
        public int AllVisitsNo { get ; set ; }
        public int DoneVisitsNo { get ; set; }
        public int CanceledVisitsNo { get; set; }
        public int ConfirmedVisitsNo { get; set; }
        public int PendingVisitsNo { get; set; }
        public int DelayedVisitsNo { get; set; }
        public int RejectedVisitsNo { get; set; }
        public int ReassignedVisitsNo { get; set; }
        public int AllChemistNo { get; set; }
        public int ActiveChemistNo { get; set; }
        public int IdleChemistNo { get; set; }
    }
}
