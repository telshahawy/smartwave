using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitAction : Entity<Guid>
    {
        public Guid VisitActionId
        {
            get => Id;
            set => Id = value;
        }
        public Guid VisitId { get; set; }
        public int VisitActionTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public Visit Visit { get; set; }
        public VisitActionType VisitActionType { get; set; }
        public int? ReasonId { get; set; }
        public Reason Reason { get; set; }
        public string Comments { get; set; }
    }
}
