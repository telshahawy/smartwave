using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitStatusType:Entity<int>
    {
        public int VisitStatusTypeId
        {
            get => Id;
            set => value = Id;
        }
        public string StatusNameEn { get; set; }
        public string StatusNameAr { get; set; }
    }
}