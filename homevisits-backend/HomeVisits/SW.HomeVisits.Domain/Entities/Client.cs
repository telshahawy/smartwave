using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Client:Entity<Guid>
    {
        //public Guid ClientId { get=>Id; set=>value=Id; }

        public Guid ClientId
        {
            get => Id;
            set => Id = value;
        }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public Guid CountryId { get; set; }
        public string URLName { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public bool IsDeleted { get; set; }
       
        public Country Country { get; set; }
      
    }
}