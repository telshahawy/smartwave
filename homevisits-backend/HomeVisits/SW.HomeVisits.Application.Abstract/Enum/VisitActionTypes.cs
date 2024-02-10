using System;
namespace SW.HomeVisits.Application.Abstract.Enum
{
    public enum VisitActionTypes
    {
        New = 1,
        Confirmed = 2,
        Done = 3,
        Cancelled = 4,
        ToCustomer = 5,
        Arrived = 6,
        AcceptAndRequestSecondVisit = 7,
        Reject = 8,
        RequestSecondVisit = 9,
        ReassignChemist = 10
    }
}
