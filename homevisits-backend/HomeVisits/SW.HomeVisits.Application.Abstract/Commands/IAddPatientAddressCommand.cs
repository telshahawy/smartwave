using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddPatientAddressCommand
    {
        Guid PatientId { get; }
        Guid PatientAddressId { get; }
        string street { get; }
        string Latitude { get; }
        string Longitude { get; }
        string LocationUrl { get; }
        string Floor { get; }
        string Flat { get; }
        string Building { get; }
        string AdditionalInfo { get; }
        Guid GeoZoneId { get; }
        Guid CreateBy { get;}
    }
}
