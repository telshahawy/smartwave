using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Country:Entity<Guid>
    {
        public Guid CountryId
        {
            get => Id;
            set =>  Id = value;
        }
        public Guid? ClientId { get; set; }
        public int Code { get; set; }
        public int MobileNumberLength { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Client Client { get; set; }
    }
}