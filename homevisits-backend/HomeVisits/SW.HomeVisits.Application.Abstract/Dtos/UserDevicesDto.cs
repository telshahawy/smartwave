using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class UserDevicesDto
    {
        public Guid UserDeviceId { get; set; }
        public Guid UserId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
