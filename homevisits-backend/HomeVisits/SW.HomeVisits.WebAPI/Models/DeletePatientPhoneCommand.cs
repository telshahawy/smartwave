using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeletePatientPhoneCommand : IDeletePatientPhoneCommand
    {
        public Guid PatientPhoneId { get; set; }
    }
}
