using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(VisitsNotificationsView), Schema = "HomeVisits")]
    public class VisitsNotificationsView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public Guid NotificationId { get; set; }

        [Column(Order = 2)]
        public string Title { get; set; }

        [Column(Order = 3)]
        public string Message { get; set; }

        [Column(Order = 4)]
        public string TitleAr { get; set; }

        [Column(Order = 5)]
        public string MessageAr { get; set; }

        [Column(Order = 6)]
        public Guid ChemistId { get; set; }

        [Column(Order = 7)]
        public DateTime CreationDate { get; set; }


    }
}
