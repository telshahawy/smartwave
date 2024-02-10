using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateChemistTrackingLogCommand : ICreateChemistTrackingLogCommand
    {
        public Guid ChemistId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public string UserName { get; set; }

    }
}
