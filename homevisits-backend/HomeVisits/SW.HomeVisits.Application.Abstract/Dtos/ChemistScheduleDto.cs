using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SW.HomeVisits.Application.Abstract.Dtos
{

    public class ChemistScheduleDto
    {
        [JsonProperty("schedule")]
        public List<Days> Schedule { get; set; }
    }

    public class Days
    {
        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("times")]
        public List<Time> Times { get; set; }
    }

    public class Time
    {
        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }
    }


}
