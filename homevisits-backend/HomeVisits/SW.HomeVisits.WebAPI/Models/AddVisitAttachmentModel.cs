using System;
using System.ComponentModel.DataAnnotations;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddVisitAttachmentModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
