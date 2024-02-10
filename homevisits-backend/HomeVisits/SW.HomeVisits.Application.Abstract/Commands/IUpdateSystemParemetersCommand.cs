using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
   public interface IUpdateSystemParemetersCommand
    {
        public Guid ClientId { get; }
        public int EstimatedVisitDurationInMin { get; }
        public int NextReserveHomevisitInDay { get; }
        public int RoutingSlotDurationInMin { get; }
        public string VisitApprovalBy { get; }
        public string VisitCancelBy { get; }
        public Guid? DefaultCountryId { get; }
        public Guid? DefaultGovernorateId { get; }

        public bool? IsSendPatientTimeConfirmation { get; }
        public bool? IsOptimizezonebefore { get; }
        public int? OptimizezonebeforeInMin { get; }
        public string CallCenterNumber { get; }
        public string WhatsappBusinessLink { get; }
        public string PrecautionsFile { get; }
        public string FileName { get; }
        public Guid CreateBy { get; }
    }
}
