using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateChemistPermitCommand
    {
        public Guid ChemistPermitId { get; set; }
        public DateTime PermitDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid ClientId { get; set; }
    }
}
