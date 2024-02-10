using SW.Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Domain.Entities
{
   public class SystemParameter: Entity<Guid>
    {
        //public SystemParameter(Guid systemParametersId)
        //{
        //    SystemParametersId = systemParametersId;
        //    //DefaultCountryId = defaultCountry;
        //    //DefaultGovernorateId = defaultGovernorate;
        //}
        [Key]
        [ForeignKey("Client")]
        // [Column(Order = 0)]
        public Guid ClientId
        {
            get => Id;
            set => Id = value;
        }
        public int EstimatedVisitDurationInMin { get; set; }
        public int NextReserveHomevisitInDay { get; set; }
        public int RoutingSlotDurationInMin { get; set; }
        public string VisitApprovalBy { get; set; }
        public string VisitCancelBy { get; set; }
        [ForeignKey("Country")]
        public Guid? DefaultCountryId { get; set; }
        [ForeignKey("Governate")]
        public Guid? DefaultGovernorateId { get; set; }
       
        public bool? IsSendPatientTimeConfirmation { get; set; }
        public bool? IsOptimizezonebefore { get; set; }
        public int? OptimizezonebeforeInMin { get; set; }
        public string CallCenterNumber { get; set; }
        public string WhatsappBusinessLink { get; set; }
        public string PrecautionsFile { get; set; }
        public string FileName { get; set; }
        public Guid CreateBy { get; set; }
        public Country Country { get; set; }
        public Governate Governate { get; set; }
        public Client Client { get; set; }
    }
}
