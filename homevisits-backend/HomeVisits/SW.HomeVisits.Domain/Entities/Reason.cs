using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Reason : Entity<int>
    {
        public int ReasonId { get => Id; set => Id = value; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int? ReasonActionId { get; set; }
        public int VisitTypeActionId { get; set; }
        public bool IsDeleted { get; set; }
        public ReasonAction ReasonAction { get; set; }
        public Client Client { get; set; }
        public VisitActionType VisitActionType { get; set; }

    }
}

