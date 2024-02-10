using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class PatientsDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public string DOB { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsTherePendingVisits { get; set; }
        public IEnumerable<PatientVistisDto> PendingVistis { get; set; }
        public IEnumerable<PatientPhoneNumbersDto> PatientPhoneNumbers { get; set; }
        public IEnumerable<PatientAddressDto> PatientAddresses { get; set; }
        public string GovernateName { get; set; }

    }
}
