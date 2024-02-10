using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateCityCommand
    {
        Guid Id { get; }
        string NameAr { get; }
        string NameEn { get; }
    }
}
