using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("SystemParametersView", Schema = "HomeVisits")]
    public class SystemParametersView
    {
        [Column(Order = 0)]
        [Key]
        public Guid ClientId { get; set; }

        [Column(Order = 1)]
        [Key]
        public int EstimatedVisitDurationInMin { get; set; }
        [Column(Order = 2)]
        [Key]
        public int NextReserveHomevisitInDay { get; set; }
        [Column(Order = 3)]
        [Key]
        public int RoutingSlotDurationInMin { get; set; }
        [Column(Order = 4)]
        [Key]
        public string VisitApprovalBy { get; set; }
        [Column(Order = 5)]
        [Key]
        public string VisitCancelBy { get; set; }
        [Column(Order = 6)]
        [Key]
        public Guid? DefaultCountryId { get; set; }
        [Column(Order = 7)]
        [Key]
        public Guid? DefaultGovernorateId { get; set; }
        
        [Column(Order = 8)]
        [Key]
        public bool? IsSendPatientTimeConfirmation { get; set; }
        [Column(Order = 9)]
        [Key]
        public bool? IsOptimizezonebefore { get; set; }
        [Column(Order = 10)]
        [Key]
        public int? OptimizezonebeforeInMin { get; set; }
        [Column(Order = 11)]
        [Key]
        public string CallCenterNumber { get; set; }
        [Column(Order = 12)]
        [Key]
        public string WhatsappBusinessLink { get; set; }
        [Column(Order = 13)]
        [Key]
        public string PrecautionsFile { get; set; }
        [Column(Order = 14)]
        [Key]
        public string FileName { get; set; }
        [Column(Order = 15)]
        [Key]
        public Guid CreateBy { get; set; }
    }
}
