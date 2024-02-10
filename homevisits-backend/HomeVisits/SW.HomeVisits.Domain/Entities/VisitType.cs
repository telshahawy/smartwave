using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitType : Entity<int>
    {
        public int VisitTypeId
        {
            get => Id; set => value = Id;
        }

        public string TypeNameEn
        {
            get; set;
        }

        public string TypeNameAr
        {
            get; set;
        }
    }
}