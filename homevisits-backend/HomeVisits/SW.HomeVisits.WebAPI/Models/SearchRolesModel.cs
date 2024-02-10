﻿using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchRolesModel: IPaggingQuery
    {
        public int? Code { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
        public string PhoneNumber { get; set; }
    }
}
