using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
   public interface IUpdateClientCommand
    {
        Guid ClientId { get; }
        Guid CountryId { get; }
        string ClientName { get; }
        string ClientCode { get; }
        string URLName { get; }

        string DisplayName { get; }
        string Logo { get; }
        bool IsActive { get; }
    }
}
