﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
  public  interface IGetUserQuery
    {
        public Guid PatientId { get; set; }
    }
}
