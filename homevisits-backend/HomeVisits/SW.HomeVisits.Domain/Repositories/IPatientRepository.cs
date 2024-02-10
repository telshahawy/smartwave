using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Domain.Repositories
{
    public interface IPatientRepository : IDisposableRepository
    {
        void AddPatientAddress(PatientAddress address);
        void AddPatientPhone(PatientPhone phone);
        int GetLatestPatientAddressCode();
        bool PatientHasPhone(Guid patientId, string phone);
        void UpdatePatientAddress(PatientAddress patientAddress);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatientAddress(Guid patientAddressId);
        void DeletePatientPhone(Guid patientPhoneId);
        void DeletePatient(Guid patientId);
    }
}
