using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddOnHoldVisitModel
    {
        public Guid TimeZoneFrameGeoZoneId { get; set; }
        public int NoOfPatients { get; set; }
        public string DeviceSerialNo { get; set; }
    }


}
