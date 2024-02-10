using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddPatientPhoneCommand
    {
        Guid PatientPhoneId { get; }
        Guid PatientId { get; }
        string Phone { get; }
        Guid CreateBy { get; }
    }
}
