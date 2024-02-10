using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddGeoZoneModel
    {
        public string AreaName { get; set; }

        public Guid GovernateId { get; set; }

        public string MappingCode { get; set; }

        public string KmlFilePath { get; set; }
        public string KmlFileName { get; set; }
        public bool IsActive { get; set; }

        public List<AddTimeZoneFramesModel> TimeZoneFrames { get; set; }

    }


}
