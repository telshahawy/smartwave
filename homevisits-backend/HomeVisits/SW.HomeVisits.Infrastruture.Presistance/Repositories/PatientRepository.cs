using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;
using System.Linq;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    public class PatientRepository : EfRepository<HomeVisitsDomainContext>, IPatientRepository
    {
        public PatientRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void AddPatient(Patient patient)
        {
            Context.Patients.Add(patient);
        }

        public void AddPatientAddress(PatientAddress address)
        {
            Context.PatientAddress.Add(address);
        }
        public void AddPatientPhone(PatientPhone phone)
        {
            Context.PatientPhones.Add(phone);
        }

        public void DeletePatient(Guid patientId)
        {
            var patient = Context.Patients.Find(patientId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            patient.IsDeleted = true;
            Context.Patients.Update(patient);
        }

        public void DeletePatientAddress(Guid patientAddressId)
        {
            var address = Context.PatientAddress.Find(patientAddressId);
            if (address == null)
            {
                throw new Exception("Address not found");
            }

            if (Context.PatientAddress.Where(a => a.PatientId == address.PatientId && !a.IsDeleted).Count() > 1)
            {
                address.IsDeleted = true;
                Context.PatientAddress.Update(address);
            }
            else
            {
                throw new Exception("At least one address is required");
            }

        }

        public void DeletePatientPhone(Guid patientPhoneId)
        {
            var phone = Context.PatientPhones.Find(patientPhoneId);
            if (phone == null)
            {
                throw new Exception("Phone not found");
            }

            if (Context.PatientPhones.Where(p => p.PatientId == phone.PatientId && !p.IsDeleted).Count() > 1)
            {
                phone.IsDeleted = true;
                Context.PatientPhones.Update(phone);
            }
            else
            {
                throw new Exception("At least one phone is required");
            }
           
        }

        public int GetLatestPatientAddressCode()
        {
            var codes = Context.PatientAddress;

            return !codes.Any() ? 0 : codes.OrderByDescending(u => u.Code).FirstOrDefault().Code;
        }

        public bool PatientHasPhone(Guid patientId, string phone)
        {
            return Context.PatientPhones.Any(x => x.PatientId == patientId && x.PhoneNumber == phone);
        }

        public void UpdatePatient(Patient patient)
        {
            Context.Patients.Update(patient);
        }

        public void UpdatePatientAddress(PatientAddress patientAddress)
        {
            Context.PatientAddress.Update(patientAddress);
        }
    }
}
