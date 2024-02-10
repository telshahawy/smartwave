using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreatePatientAuthModel : ICreatePatientAuthCommand
    {
        public Guid UserId { get ; set; }
        public string UserName { get ; set; }
        public string PhoneNo { get ; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid ClientId { get ; set; }
        public bool IsActive { get ; set; }
        public Guid CreatedBy { get; set; }
        public Guid RoleId { get; set; }
        public int Code { get; set; }
    }
}
