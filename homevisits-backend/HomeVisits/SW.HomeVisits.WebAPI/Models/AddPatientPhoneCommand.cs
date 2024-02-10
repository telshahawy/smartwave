using System;
using SW.HomeVisits.Application.Abstract.Commands;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientPhoneCommand:IAddPatientPhoneCommand
    {
        public Guid PatientPhoneId { get; set; }
        public Guid PatientId { get; set; }
        public string Phone { get; set; }
        public Guid CreateBy { get; set; }
    }
}
