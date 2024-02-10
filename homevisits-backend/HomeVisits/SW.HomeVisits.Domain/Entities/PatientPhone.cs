using System;
using SW.Framework.Domain;
namespace SW.HomeVisits.Domain.Entities
{
    public class PatientPhone:Entity<Guid>
    {
        public Guid PatientPhoneId { get => Id; set =>Id= value; }
        public Guid PatientId { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CreateBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
