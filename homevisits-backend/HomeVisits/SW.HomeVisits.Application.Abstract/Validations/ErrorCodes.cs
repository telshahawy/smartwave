using System;
namespace SW.HomeVisits.Application.Abstract.Validations
{
    public enum ErrorCodes : int
    {
        None = 0,
        RecordDoesNotExist = 1,
        RequestIsEmpty = 2,
        UnknownErrorCode = 3,
        PhoneNumberAlreadyExists = 4,
        RoleNameAlreadyExists = 5,
        NotFound = 6,
        DateTimeGreaterThanToday = 9,
        CountryNameAlreadyExists = 7,
        GovernateNameAlreadyExists = 8,
    }
}
