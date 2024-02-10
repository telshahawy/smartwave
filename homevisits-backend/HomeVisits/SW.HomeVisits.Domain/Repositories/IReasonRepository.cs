using System;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IReasonRepository : IDisposableRepository
    {
        void PresistNewReason(Reason reason);

        void UpdateReason(Reason reason);

        Reason GetReasonById(int reasonId);

        void DeleteReason(int reasonId);
    }
}
