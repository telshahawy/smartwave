using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddUserDeviceModel
    {
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }

    }
}
