using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateSystemParametersCommand : ICreateSystemParametersCommand
    {
        public Guid ClientId { get; set; }
        public int EstimatedVisitDurationInMin { get; set; }
        public int NextReserveHomevisitInDay { get; set; }
        public int RoutingSlotDurationInMin { get; set; }
        public string VisitApprovalBy { get; set; }
        public string VisitCancelBy { get; set; }
        public Guid? DefaultCountryId { get; set; }
        public Guid? DefaultGovernorateId { get; set; }
        public bool? IsSendPatientTimeConfirmation { get; set; }
        public bool? IsOptimizezonebefore { get; set; }
        public int? OptimizezonebeforeInMin { get; set; }
        public string CallCenterNumber { get; set; }
        public string WhatsappBusinessLink { get; set; }
        public string PrecautionsFile { get; set; }
        public Guid CreateBy { get; set; }
        public string FileName { get; set ; }
        //public Guid Client { get; set; }
    }
}
