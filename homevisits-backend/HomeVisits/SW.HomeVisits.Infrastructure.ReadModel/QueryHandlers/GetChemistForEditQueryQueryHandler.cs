using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistForEditQueryQueryHandler : IQueryHandler<IGetChemistForEditQuery, IGetChemistForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistForEditQueryQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistForEditQueryResponse Read(IGetChemistForEditQuery query)
        {
            IQueryable<ChemistsView> dbQuery = _context.ChemistsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var chemistQuery = dbQuery.Where(x => x.ChemistId == query.ChemistId && x.IsDeleted != true).ToList().GroupBy(x => x.ChemistId);
            if (!chemistQuery.Any())
            {
                throw new Exception("Chemist not found");
            }
            return new GetChemistForEditQueryResponse()
            {
                Chemist = chemistQuery.Select(x => new ChemistWithAssignedGeoZonesDto
                {
                    ChemistId = x.Key,
                    Age = DateTime.Now.Year - x.First().BirthDate.GetValueOrDefault().Year,
                    Code = x.First().Code,
                    Gender = x.First().Gender,
                    IsActive = x.First().IsActive,
                    JoinDate = x.First().JoinDate,
                    Name = x.First().Name,
                    PhoneNumber = x.First().PhoneNumber,
                    ClientId = x.First().ClientId,
                    Birthdate = x.First().BirthDate.GetValueOrDefault(),
                    ExpertChemist = x.First().ExpertChemist,
                    PersonalPhoto = x.First().PersonalPhoto,
                    UserName = x.First().UserName,
                    GeoZones = x.Where(s=> s.GeoZoneIsActive == true && s.GeoZoneIsDeleted != true && s.AssignedGeoZoneIsDeleted != true &&
                    s.CountryIsActive == true && s.CountryIsDeleted != true && s.GovernateIsActive == true && s.GovernateIsDeleted != true).Select(g => new ChemistAssignedGeoZoneDto
                    {
                       GeoZoneId = g.GeoZoneId.GetValueOrDefault(),
                       ChemistId = x.Key,
                       GovernateId =g.GovernateId.GetValueOrDefault(),
                       CountryId = g.CountryId.GetValueOrDefault(),
                       IsActive = g.GeoZoneIsActive.GetValueOrDefault(),
                       GovernateIsActive = g.GovernateIsActive.GetValueOrDefault(),
                       CountryIsActive = g.CountryIsActive.GetValueOrDefault()
                       
                    }).ToList()
                }).Single(),
            } as IGetChemistForEditQueryResponse;
        }
    }
}
