using System;
using System.ComponentModel.DataAnnotations;

namespace SW.HomeVisits.WebAPI.Models
{
    public class VisitDetailsModel
    {
        [Required]
        public string VisitId { get; set; }

    }
}
