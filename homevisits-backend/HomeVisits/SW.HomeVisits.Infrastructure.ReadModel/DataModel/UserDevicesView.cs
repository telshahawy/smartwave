using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(UserDevicesView), Schema = "HomeVisits")]
    public class UserDevicesView
    {

        [Column(Order = 0)]
        [Key]
        public Guid UserDeviceId { get; set; }

        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public string DeviceSerialNumber { get; set; }

        [Column(Order = 3)]
        public string FireBaseDeviceToken { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedAt { get; set; }

    }
}
