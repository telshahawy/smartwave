using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateReasonModel
    {
        [Required]
        public string ReasonName { get; set; }

        [Required]
        public int VisitTypeActionId { get; set; }
        public int? ReasonActionId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
