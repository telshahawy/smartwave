using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreatePatientAuthCommand
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        //public Guid RoleId { get; set; }
        public int Code { get; set; }

    }
}
