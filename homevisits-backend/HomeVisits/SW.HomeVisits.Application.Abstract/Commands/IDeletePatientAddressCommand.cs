using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeletePatientAddressCommand
    {
        Guid PatientAddressId { get; set; }
    }
}
