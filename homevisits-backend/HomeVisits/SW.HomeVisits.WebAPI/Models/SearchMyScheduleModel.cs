using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchMyScheduleModel
    {
        public string visitDate { get; set; }
        public string order { get; set; } = "ASEC";
       

    }
}
