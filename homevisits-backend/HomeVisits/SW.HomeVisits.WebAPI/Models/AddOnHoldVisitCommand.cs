using System;
using SW.HomeVisits.Application.Abstract.Commands;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddOnHoldVisitCommand : IAddOnHoldVisitCommand
    {
        public Guid OnHoldVisitId { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid TimeZoneFrameGeoZoneId { get; set; }
        public int NoOfPatients { get; set; }
        public string DeviceSerialNo { get; set; }
    }
}
