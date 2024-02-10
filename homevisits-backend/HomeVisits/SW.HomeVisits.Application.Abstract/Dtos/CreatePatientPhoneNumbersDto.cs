using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class CreatePatientPhoneNumbersDto
    {
        public Guid PatientPhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid PatientId { get; set; }

    }
}
