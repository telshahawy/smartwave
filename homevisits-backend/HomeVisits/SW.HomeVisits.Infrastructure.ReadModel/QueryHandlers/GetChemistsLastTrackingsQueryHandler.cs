using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
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
    internal class GetChemistsLastTrackingsQueryHandler : IQueryHandler<IGetChemistsLastTrackingsQuery, IGetChemistsLastTrackingsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistsLastTrackingsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public IGetChemistsLastTrackingsQueryResponse Read(IGetChemistsLastTrackingsQuery query)
        {
            List<ChemistLastTrackingLogDto> result = new List<ChemistLastTrackingLogDto>();
            IQueryable<ChemistsLastTrackingLogView> dbQuery = _context.ChemistsLastTrackingLogViews;


            var data = dbQuery.Select(x => new ChemistLastTrackingLogDto()
            {
                ChemistId = x.ChemistId,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                LastTrackingTime = x.CreationDate,
                MobileBatteryPercentage = x.MobileBatteryPercentage,
                VisitNo = x.VisitTime.HasValue ? x.VisitNo.ToString() : "",
                VisitDate = x.VisitDate,
                VisitTime = x.VisitTime,
                AreaName = query.CultureName == CultureNames.ar? x.AreaNameAr : x.AreaNameEN

            }).AsEnumerable().GroupBy(x => x.ChemistId).Select(x => x.First());

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Name))
                {
                    data = data.Where(x => x.Name.Contains(query.Name, StringComparison.InvariantCultureIgnoreCase));
                }
}
var totalCount = data.Count();

if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
{
    int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
    result = data.Skip(skipRows).Take(query.PageSize.Value).ToList();
}
return new GetChemistsLastTrackingsQueryResponse()
{
    ChemistLastTrackingLogs = result,
    CurrentPageIndex = query.CurrentPageIndex,
    TotalCount = totalCount,
    PageSize = query.PageSize
};
        }
    }
}
