using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(PatientPhoneNumbersView), Schema = "HomeVisits")]
    public class PatientPhoneNumbersView
    {

        [Column(Order = 0)]
        public Guid PatientId { get; set; }

        [Column(Order = 1)]
        public Guid PatientPhoneId { get; set; }

        [Column(Order =2)]
        public string PhoneNumber { get; set; }

        [Column(Order = 3)]
        public DateTime CreatedAt { get; set; }
    }
}
