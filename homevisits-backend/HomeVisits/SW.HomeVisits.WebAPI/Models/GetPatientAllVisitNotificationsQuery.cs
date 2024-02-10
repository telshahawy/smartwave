﻿using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetPatientAllVisitNotificationsQuery : IGetPatientAllVisitNotificationsQuery
    {
        public Guid PatientId { get ; set ; }

        public CultureNames CultureName { get; set; }
    }
}
