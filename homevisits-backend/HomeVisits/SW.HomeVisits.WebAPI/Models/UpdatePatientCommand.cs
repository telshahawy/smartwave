using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdatePatientCommand : IUpdatePatientCommand
    {
        public Guid PatientId { get; set; }

        public string PatientNo { get; set; }

        public string DOB { get; set; }

        public int Gender { get; set; }

        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsDeleted { get; set; }
        public List<CreatePatientAddressDto> Addresses { get; set; }
        public List<CreatePatientPhoneNumbersDto> Phones { get; set; }
    }
}
