using Microsoft.EntityFrameworkCore;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
   internal class SystemParametersRepository : EfRepository<HomeVisitsDomainContext>, ISystemParametersRepository
    {
        public SystemParametersRepository(HomeVisitsDomainContext context) : base(context)
        {

        }

        public List<SystemParameter> GetSystemParameter()
        {
            return Context.SystemParameters.ToList();
        }

        public SystemParameter GetSystemParameterByClientId(Guid ClientId)
        {
            return Context.SystemParameters.SingleOrDefault(x=>x.ClientId==ClientId);
        }

       
        public void PresistNewSystemParameter(SystemParameter systemParameter)
        {
            Context.SystemParameters.Add(systemParameter);
        }

        public void UpdateSystemParameter(SystemParameter systemParameter)
        {
            Context.SystemParameters.Update(systemParameter);

            //Context.Entry(systemParameter).State = EntityState.Modified;
            ////the entity is being tracked by the context and exists in the database, and some or all of its property values have been modified
            //Context.SaveChanges();
            //Modified entities are updated in the database and then become Unchanged when SaveChanges returns.

        }
    }
}
