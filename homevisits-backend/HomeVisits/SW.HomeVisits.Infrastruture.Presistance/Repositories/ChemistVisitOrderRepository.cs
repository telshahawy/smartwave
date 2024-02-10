using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class ChemistVisitOrderRepository : EfRepository<HomeVisitsDomainContext>, IChemistVisitOrderRepository
    {
        public ChemistVisitOrderRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void AddChemistVisitOrder(ChemistVisitOrder chemistVisitOrder)
        {
            Context.ChemistVisitOrder.Add(chemistVisitOrder);
        }

        public void AddChemistVisitOrder(List<ChemistVisitOrder> chemistVisitOrders)
        {
            Context.ChemistVisitOrder.AddRange(chemistVisitOrders);
        }

        public void DeleteChemistVisitOrder(ChemistVisitOrder chemistVisitOrder)
        {
            chemistVisitOrder.IsDeleted = true;
            Context.ChemistVisitOrder.Update(chemistVisitOrder);
        }

        public void DeleteChemistVisitOrder(List<ChemistVisitOrder> chemistVisitOrders)
        {
            chemistVisitOrders.ForEach(p => p.IsDeleted = true);
            Context.ChemistVisitOrder.UpdateRange(chemistVisitOrders);
        }

        public ChemistVisitOrder GetChemistVisitOrderById(Guid chemistVisitOrderId)
        {
            var chemistVisitOrder = Context.ChemistVisitOrder.Find(chemistVisitOrderId);
            Context.Entry(chemistVisitOrder).Reference(x => x.Visit).Load();
            Context.Entry(chemistVisitOrder).Reference(x => x.Chemist).Load();
            Context.Entry(chemistVisitOrder).Reference(x => x.TimeZoneFrame).Load();
            return chemistVisitOrder;
        }

        public IQueryable<ChemistVisitOrder> GetChemistVisitOrderByVisitId(Guid visitId)
        {
            var chemistVisitOrder = Context.ChemistVisitOrder.Where(p => p.VisitId == visitId).Include(p => p.TimeZoneFrame).Include(p => p.Chemist).Include(p => p.Visit);
            return chemistVisitOrder;
        }

        public IQueryable<ChemistVisitOrder> GetChemistVisitOrderByChemistId(Guid chemistId)
        {
            var chemistVisitOrder = Context.ChemistVisitOrder.Where(p => p.ChemistId == chemistId).Include(p => p.TimeZoneFrame).Include(p => p.Chemist).Include(p => p.Visit);
            return chemistVisitOrder;
        }

        public IQueryable<ChemistVisitOrder> GetChemistVisitOrderByTimeZoneFrameId(Guid timeZoneFrameId)
        {
            var chemistVisitOrder = Context.ChemistVisitOrder.Where(p => p.TimeZoneFrameId == timeZoneFrameId).Include(p => p.TimeZoneFrame).Include(p => p.Chemist).Include(p => p.Visit);
            return chemistVisitOrder;
        }

        public IQueryable<ChemistVisitOrder> GetChemistVisitOrder(Guid chemistId, Guid visitId)
        {
            var chemistVisitOrder = Context.ChemistVisitOrder.Where(p => p.ChemistId == chemistId && p.VisitId == visitId).Include(p => p.TimeZoneFrame).Include(p => p.Chemist).Include(p => p.Visit);
            return chemistVisitOrder;
        }
    }
}
