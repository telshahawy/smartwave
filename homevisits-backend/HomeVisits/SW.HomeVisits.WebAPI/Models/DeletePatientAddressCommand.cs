using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeletePatientAddressCommand : IDeletePatientAddressCommand
    {
        public Guid PatientAddressId { get; set; }
    }
}
