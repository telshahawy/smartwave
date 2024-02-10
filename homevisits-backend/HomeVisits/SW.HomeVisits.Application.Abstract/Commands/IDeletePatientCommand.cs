using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeletePatientCommand
    {
        Guid PatientId { get; set; }
    }
}
