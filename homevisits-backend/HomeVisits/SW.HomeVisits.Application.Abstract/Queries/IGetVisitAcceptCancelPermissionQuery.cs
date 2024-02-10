using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetVisitAcceptCancelPermissionQuery
    {
        public Guid ClientId { get; set; }
        public string IsCancelledBy { get; set; }
        public string IsAcceptedBy { get; set; }
    }
}
