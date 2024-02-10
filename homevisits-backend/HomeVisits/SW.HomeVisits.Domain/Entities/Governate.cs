using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Governate:Entity<Guid>
    {
        public Guid GovernateId
        {
            get => Id;
            set => Id = value;
        }
        public string GoverNameEn { get; set; }
        public string GoverNameAr { get; set; }
        public Guid CountryId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Country Country { get; set; }
        public int Code { get; set; }
        public string CustomerServiceEmail { get; set; }

    }
}