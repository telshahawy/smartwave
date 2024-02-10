using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IClientRepository: IDisposableRepository
    {
        void AddClient(Client client);
        void UpdateClient(Client client);
        Client GetClientByID(Guid ClientId);
        void DeleteClient(Guid ClientId);



    }
}
