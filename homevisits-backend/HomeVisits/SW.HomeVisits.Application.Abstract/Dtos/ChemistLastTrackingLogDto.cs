using SW.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistLastTrackingLogDto
    {
        public Guid ChemistId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastTrackingTime { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public string VisitNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public string AreaName { get; set; }
        public string LocationUrl
        {
            get
            {
                return GoogleMapsUtility.LocationUrl(Latitude, Longitude);
            }
        }
        public string TimePassed
        {
            get { return LastTrackingTime.TimePassed(); }
        }
    }
}
