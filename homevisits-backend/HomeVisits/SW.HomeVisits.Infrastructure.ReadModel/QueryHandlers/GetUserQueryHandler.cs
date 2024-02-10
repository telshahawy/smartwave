using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetUserQueryHandler : IQueryHandler<IGetUserQuery, IIsUserExist>
    {
        private readonly HomeVisitsReadModelContext _context;

        public GetUserQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }

        public IIsUserExist Read(IGetUserQuery query)
        {
            if (query.PatientId == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            IQueryable<UserView> dbQuery = _context.UserViews;

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.IsActive == true &&x.UserName==query.PatientId.ToString()
                );
            if (dbQuery == null) {
                return new IsUserExist() {IsExist=false } as IIsUserExist; }
            return  new IsUserExist() {IsExist=true } as IIsUserExist;
        }
    }
}
