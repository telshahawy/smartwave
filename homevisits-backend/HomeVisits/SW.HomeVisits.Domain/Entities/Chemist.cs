using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Chemist:Entity<Guid>
    {
        public Guid ChemistId
        {
            get => Id;
            set => Id = value;
        }
        public int Code { get; set; }
        public int DOB { get; set; }
        public bool ExpertChemist { get; set; }
        public DateTime JoinDate { get; set; }
        public User user { get; set; }
        public ICollection<ChemistAssignedGeoZone> ChemistsGeoZones{ get; set; }
        public ICollection<ChemistPermit> ChemistPermits{ get; set; }
    }
}