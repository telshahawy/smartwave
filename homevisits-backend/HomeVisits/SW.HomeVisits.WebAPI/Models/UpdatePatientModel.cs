using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdatePatientModel
    {
        public Guid PatientId { get; set; }
        public string PatientNo { get; set; }

        public string DOB { get; set; }

        public int Gender { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
        public List<UpdatePatientAddressModel> Addresses { get; set; }
        public List<UpdatePatientPhoneModel> Phones { get; set; }

    }


}
