using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteReasonCommand : IDeleteReasonCommand
    {
        public int ReasonId { get; set; }
    }
}
