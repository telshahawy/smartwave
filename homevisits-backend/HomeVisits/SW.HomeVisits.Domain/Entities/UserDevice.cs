using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class UserDevice : Entity<Guid>
    {
        public Guid UserDeviceId
        {
            get => Id;
            set => Id = value;
        }
        public Guid UserId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string FireBaseDeviceToken { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}