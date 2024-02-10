using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddUserDeviceCommand
    {
        public Guid UserDeviceId { get; set; }
        public Guid UserId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }
    }
}
