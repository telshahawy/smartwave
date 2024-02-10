using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(PatientSearchView), Schema = "HomeVisits")]
    public class PatientSearchView
    {
        [Column(Order = 0)]
        public Guid PatientId { get; set; }

        [Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public string PhoneNumber { get; set; }

        [Column(Order = 3)]
        public int Gender { get; set; }

        [Column(Order = 4)]
        public string DOB { get; set; }

        [Column(Order = 4)]
        public Guid ClientId { get; set; }

        [Column(Order = 5)]
        public DateTime? BirthDate { get; set; }
     
    }
}
