using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddPatientCommand
    {
        Guid PatientId { get; }
        string DOB { get; }
        int Gender { get; }
        Guid ClientId { get; }
        string Name { get; }
        DateTime? BirthDate { get; }
        bool IsDeleted { get; }
        public List<CreatePatientAddressDto> Addresses { get; set; }
        public List<CreatePatientPhoneNumbersDto> Phones { get; set; }

    }
}
