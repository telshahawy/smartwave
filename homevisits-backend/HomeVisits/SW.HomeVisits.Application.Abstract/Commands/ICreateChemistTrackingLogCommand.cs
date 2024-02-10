using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateChemistTrackingLogCommand
    {
        public Guid ChemistId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public string UserName { get; set; }
    }
}
