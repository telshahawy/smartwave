using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class ReasonAction : Entity<int>
    {
        public int ReasonActionId { get => Id; set => Id = value; }
        public string Name { get; set; }

    }
}

