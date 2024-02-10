﻿using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAvailableVisitsForChemistQuery : IGetAvailableVisitsForChemistQuery
    {
        public Guid ChemistId {get;set;}

        public Guid? GeoZoneId {get;set;}

        public DateTime Date {get;set;}

        public Guid ClientId {get;set;}

        public CultureNames CultureName { get; set; } = CultureNames.en;
    }
}
