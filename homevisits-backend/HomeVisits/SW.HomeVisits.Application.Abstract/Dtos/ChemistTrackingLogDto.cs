using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistTrackingLogDto
    {
        public Guid ChemistTrackingLogId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime CreationDate { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public string UserName { get; set; }

    }
}
