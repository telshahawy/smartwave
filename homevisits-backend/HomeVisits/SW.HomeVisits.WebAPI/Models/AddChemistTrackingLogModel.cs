using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddChemistTrackingLogModel
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
    }
}
