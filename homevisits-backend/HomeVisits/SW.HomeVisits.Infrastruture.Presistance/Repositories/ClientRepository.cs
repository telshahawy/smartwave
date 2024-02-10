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
   public  class ClientRepository : EfRepository<HomeVisitsDomainContext>, IClientRepository
    {
        public ClientRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void AddClient(Client client)
        {
            Context.Clients.Add(client);
        }

        public void DeleteClient(Guid ClientId)
        {
            var client = Context.Clients.Find(ClientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            }

            if (Context.Patients.Any(g => g.ClientId == ClientId))
            {
                throw new Exception("Client is already having linked data!");
            }
            else
            {
                client.IsDeleted = true;
            }
            Context.Clients.Update(client);
        }

        public Client GetClientByID(Guid ClientId)
        {
            return Context.Clients.SingleOrDefault(a => a.ClientId == ClientId);
        }

        public void UpdateClient(Client client)
        {
            Context.Clients.Update(client);
        }

    }
}
