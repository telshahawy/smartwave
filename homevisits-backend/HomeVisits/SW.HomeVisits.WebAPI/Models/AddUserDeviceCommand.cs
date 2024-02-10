using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddUserDeviceCommand : IAddUserDeviceCommand
    {
        public Guid UserDeviceId { get; set; }
        public Guid UserId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }
    }
}
