using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IChemistVisitOrderRepository : IDisposableRepository
    {
        void AddChemistVisitOrder(ChemistVisitOrder chemistVisitOrder);
        void AddChemistVisitOrder(List<ChemistVisitOrder> chemistVisitOrders);

        void DeleteChemistVisitOrder(ChemistVisitOrder chemistVisitOrder);
        void DeleteChemistVisitOrder(List<ChemistVisitOrder> chemistVisitOrders);

        ChemistVisitOrder GetChemistVisitOrderById(Guid chemistVisitOrderId);
        IQueryable<ChemistVisitOrder> GetChemistVisitOrderByVisitId(Guid visitId);
        IQueryable<ChemistVisitOrder> GetChemistVisitOrderByChemistId(Guid chemistId);
        IQueryable<ChemistVisitOrder> GetChemistVisitOrder(Guid chemistId, Guid visitId);
        IQueryable<ChemistVisitOrder> GetChemistVisitOrderByTimeZoneFrameId(Guid timeZoneFrameId);
    }
}
