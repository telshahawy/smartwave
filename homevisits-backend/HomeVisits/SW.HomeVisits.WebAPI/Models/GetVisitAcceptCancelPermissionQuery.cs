using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetVisitAcceptCancelPermissionQuery : IGetVisitAcceptCancelPermissionQuery
    {
        public Guid ClientId { get ; set; }
        public string IsCancelledBy { get ; set; }
        public string IsAcceptedBy { get; set; }
    }
}
