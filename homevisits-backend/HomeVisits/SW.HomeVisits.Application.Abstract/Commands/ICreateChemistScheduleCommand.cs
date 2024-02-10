using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateChemistScheduleCommand
    {
        Guid ChemistId { get; }
        Guid ChemistScheduleId { get; }
        Guid AssignedChemistGeoZoneId { get; }
        float StartLatitude { get; }
        float StartLangitude { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        TimeSpan? SunStart { get; }
        TimeSpan? SunEnd { get; }
        TimeSpan? MonStart { get; }
        TimeSpan? MonEnd { get; }
        TimeSpan? TueStart { get; }
        TimeSpan? TueEnd { get; }
        TimeSpan? WedStart { get; }
        TimeSpan? WedEnd { get; }
        TimeSpan? ThuStart { get; }
        TimeSpan? ThuEnd { get; }
        TimeSpan? FriStart { get; }
        TimeSpan? FriEnd { get; }
        TimeSpan? SatStart { get; }
        TimeSpan? SatEnd { get; }
        Guid CreatedBy { get; }
    }
}
