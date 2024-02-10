using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class GetSystemParametersByClientIdForEditQuery : IGetSystemParametersByClientIdForEditQuery
    {
        public Guid ClientId { get; set; }
    }
}
