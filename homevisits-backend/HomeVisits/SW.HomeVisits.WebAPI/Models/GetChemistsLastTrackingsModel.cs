﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistsLastTrackingsModel
    {
        public string Name { get; set; }
        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
