using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ReasonsDto
    {
        public int ReasonId { get; set; }
        public string ReasonName { get; set; }
        public bool IsActive { get; set; }
        public int? ReasonActionId { get; set; }
        public string ReasonActionName { get; set; }
    }
}