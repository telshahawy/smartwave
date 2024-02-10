using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientModel
    {
        public string DOB { get; set; }

        public int Gender { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
        public List<AddPatientAddressModel> Addresses { get; set; }
        public List<AddPatientPhoneModel> Phones { get; set; }

    }


}
