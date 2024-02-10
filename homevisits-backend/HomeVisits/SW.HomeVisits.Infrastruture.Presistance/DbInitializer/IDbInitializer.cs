﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.DbInitializer
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }
}
