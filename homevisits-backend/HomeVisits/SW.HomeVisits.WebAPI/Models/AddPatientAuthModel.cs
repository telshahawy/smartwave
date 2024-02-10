using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientAuthModel
    {
        public Guid PatientId { get; set; }
        public Guid ClientId { get; set; }
        public Guid RoleId { get; set; }
        public string PatientName { get; set; }
        //public string PatientMobile { get; set; }
        public int DOB { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public List<AddPatientPhoneModel> Phones { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }
    }
}
