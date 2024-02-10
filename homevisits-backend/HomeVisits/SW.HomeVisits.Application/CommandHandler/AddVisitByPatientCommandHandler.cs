using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class AddVisitByPatientCommandHandler : ICommandHandler<IAddVisitByPatientCommand>
    {
        private readonly ILog _log;
        private readonly IHomeVisitsUnitOfWork _unitOfWork;

        public AddVisitByPatientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _log = log;
            _unitOfWork = unitOfWork;
        }

        public void Handle(IAddVisitByPatientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));

                var repository = _unitOfWork.Repository<IVisitRepository>();
                var visitLatestNo = repository.GetLatestVisitNO() + 1;
                var visitLatestCode = repository.GetLatestVisitCode() + 1;

                TimeSpan? visitTime = null;
                if (!string.IsNullOrEmpty(command.VisitTime))
                {
                    visitTime = TimeSpan.Parse(command.VisitTime);
                }

                var visit = new Visit
                {
                    VisitId = command.VisitId,
                    VisitNo = visitLatestNo.ToString(),
                    VisitCode = visitLatestCode,
                    VisitTypeId = command.VisitTypeId,
                    VisitDate = command.VisitDate,
                    PatientId = command.PatientId,
                    PatientAddressId = command.PatientAddressId,
                    ChemistId = command.ChemistId,
                    CreatedBy = command.CreatedBy,
                    CreatedDate = DateTime.Now,
                    TimeZoneGeoZoneId = command.TimeZoneGeoZoneId,
                    PlannedNoOfPatients = command.PlannedNoOfPatients,
                    RequiredTests = command.RequiredTests,
                    Comments = command.Comments,
                    SelectBy = command.SelectBy,
                    VisitTime = visitTime,
                    IamNotSure = null,
                    RelativeAgeSegmentId = command.RelativeAgeSegmentId,
                    RelativeDateOfBirth = command.RelativeDateOfBirth,
                    RelativeGender = command.RelativeGender,
                    RelativeName = command.RelativeName,
                    RelativePhoneNumber = command.RelativePhoneNumber,
                    VisitStatuses = new List<VisitStatus> {
                        new VisitStatus(Guid.NewGuid(), command.VisitId, null, null, null, null, (int)VisitActionTypes.New,
                        (int)VisitStatusTypes.New, DateTime.Now, null, null, null, null, null, command.CreatedBy)
                    },
                    Attachments = command.Attachments.Select(a => new Attachment
                    {
                        AttachmentId = a.AttachmentId,
                        VisitId = command.VisitId,
                        FileName = a.FileName,
                        FilePath = a.FilePath,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = DateTime.Now
                    }).ToList()
                };

                repository.AddVisit(visit);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
