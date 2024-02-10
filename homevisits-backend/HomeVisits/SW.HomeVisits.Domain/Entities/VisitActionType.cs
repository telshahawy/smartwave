using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitActionType:Entity<int>
    {
        public int VisitActionTypeId
        {
            get => Id;
            set => Id = value;
        }
        public string ActionNameEn { get; set; }
        public string ActionNameAr { get; set; }
    }
}