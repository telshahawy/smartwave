using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(SystemPagesView), Schema = "HomeVisits")]
    public class SystemPagesView
    {
        public int SystemPageId { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? Position { get; set; }
        public int? ParentId { get; set; }
        public bool HasURL { get; set; } // if true that mean it is page, else that mean it is menue or sub menue
        public bool IsDisplayInMenue { get; set; }
    }
}
