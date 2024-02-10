using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistTrackingLog : Entity<Guid>
    {
        public ChemistTrackingLog(Guid chemistTrackingLogId, Guid chemistId, float longitude, float latitude, string deviceSerialNumber,
            int mobileBatteryPercentage, string userName, DateTime creationDate)
        {
            ChemistTrackingLogId = chemistTrackingLogId;
            ChemistId = chemistId;
            Longitude = longitude;
            Latitude = latitude;
            DeviceSerialNumber = deviceSerialNumber;
            MobileBatteryPercentage = mobileBatteryPercentage;
            UserName = userName;
            CreationDate = creationDate;
        }

        public Guid ChemistTrackingLogId
        {
            get => Id;
            set => Id = value;
        }
        public Guid ChemistId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public Chemist Chemist { get; set; }
    }
}
