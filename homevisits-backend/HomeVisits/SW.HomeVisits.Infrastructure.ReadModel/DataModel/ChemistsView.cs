using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistsView), Schema = "HomeVisits")]
    public class ChemistsView
    {
        [Key]
        public Guid ChemistId { get; set; }
        [Key]
        public int Code { get; set; }
        [Key]
        public int DOB { get; set; }
        [Key]
        public bool ExpertChemist { get; set; }
        [Key]
        public DateTime JoinDate { get; set; }
        [Key]
        public Guid ClientId { get; set; }
        [Key]
        public string Name { get; set; }
        [Key]
        public string UserName { get; set; }
        [Key]
        public int Gender { get; set; }
        [Key]
        public string NormalizedUserName { get; set; }
        [Key]
        public string Email { get; set; }
        [Key]
        public string NormalizedEmail { get; set; }
        [Key]
        public string PhoneNumber { get; set; }
        [Key]
        public DateTime? BirthDate { get; set; }
        [Key]
        public string PersonalPhoto { get; set; }
        [Key]
        public int UserCreationTypes { get; set; }
        [Key]
        public Guid? CreatedBy { get; set; }
        [Key]
        public DateTime CreatedAt { get; set; }
        [Key]
        public bool IsActive { get; set; }
        [Key]
        public bool IsDeleted { get; set; }
        [Key]
        public Guid? GeoZoneId { get; set; }
        [Key]
        public string GeoZoneNameAr { get; set; }
        [Key]
        public string GeoZoneNameEn { get; set; }
        [Key]
        public bool? GeoZoneIsActive { get; set; }
        [Key]
        public bool? GeoZoneIsDeleted { get; set; }
        [Key]
        public Guid? GovernateId { get; set; }
        [Key]
        public string GoverNameAr { get; set; }
        [Key]
        public string GoverNameEn { get; set; }
        [Key]
        public bool? GovernateIsActive { get; set; }
        [Key]
        public bool? GovernateIsDeleted { get; set; }
        [Key]
        public Guid? CountryId { get; set; }
        [Key]
        public string CountryNameAr { get; set; }
        [Key]
        public string CountryNameEn { get; set; }
        [Key]
        public bool? CountryIsActive { get; set; }
         [Key]
        public bool? CountryIsDeleted { get; set; }
        [Key]
        public bool? AssignedGeoZoneIsDeleted { get; set; }
        

    }
}
