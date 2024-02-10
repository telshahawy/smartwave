using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistRoutesModel 
    {

        public float? StartLatitude { get; set; }

        public float? StartLongitude { get; set; }
    }
}
