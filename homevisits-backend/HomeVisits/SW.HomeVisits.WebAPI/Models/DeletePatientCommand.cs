using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeletePatientCommand : IDeletePatientCommand
    {
        public Guid PatientId { get; set; }
    }
}
