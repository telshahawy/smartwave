using System;
using System.ComponentModel.DataAnnotations;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientPhoneModel
    {
        [Required(ErrorMessage = "Patient is required")]
        public Guid PatientId { get; set; }
        [Required(ErrorMessage = "phone is required")]
        public string Phone { get; set; }
    }
}
