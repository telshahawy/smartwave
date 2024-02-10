using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeletePatientPhoneCommand
    {
        Guid PatientPhoneId { get; set; }
    }
}
