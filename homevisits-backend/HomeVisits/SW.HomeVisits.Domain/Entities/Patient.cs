using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Patient:Entity<Guid>
    {
        public Guid PatientId
        {
            get => Id;
            set => Id = value;
        }
        public string PatientNo { get; set; }
        public string DOB { get; set; }
        public int Gender { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsDeleted { get; set; }
        public Client Client { get; set; }
        public ICollection<PatientAddress> Addresses { get; set; }
        public ICollection<PatientPhone> Phones { get; set; }
    }
}