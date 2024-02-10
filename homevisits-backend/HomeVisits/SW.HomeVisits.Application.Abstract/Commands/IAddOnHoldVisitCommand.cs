using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddOnHoldVisitCommand
    {
        public Guid OnHoldVisitId { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid TimeZoneFrameGeoZoneId { get; set; }
        public int NoOfPatients { get; set; }
        public string DeviceSerialNo { get; set; }
    }
}
