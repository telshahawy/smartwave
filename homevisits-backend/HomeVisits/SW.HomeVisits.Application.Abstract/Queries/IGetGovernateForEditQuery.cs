using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetGovernateForEditQuery
    {
        public Guid GovernateId { get; set; }
    }
}
