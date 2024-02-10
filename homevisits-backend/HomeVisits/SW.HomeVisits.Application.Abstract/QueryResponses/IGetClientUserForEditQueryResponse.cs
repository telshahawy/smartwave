using System;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetClientUserForEditQueryResponse
    {
        public ClientUserDto User { get; set; }
    }
}
