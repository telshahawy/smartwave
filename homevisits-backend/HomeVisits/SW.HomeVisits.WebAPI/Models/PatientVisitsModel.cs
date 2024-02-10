using System;
using System.ComponentModel.DataAnnotations;

namespace SW.HomeVisits.WebAPI.Models
{
    public class PatientVisitsModel
    {
        [Required]
        public string PatientId { get; set; }

    }
}
