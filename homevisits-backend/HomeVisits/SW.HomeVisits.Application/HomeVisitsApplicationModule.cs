using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Cqrs;
using SW.Framework.Decorators;
using SW.Framework.DependencyInjectionCore;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.CommandHandler;
using SW.HomeVisits.Application.Validations;
using SW.HomeVisits.Application.IdentityManagement;
using IdentityServer4.Services;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Application.Notification;
using SW.HomeVisits.Application.Abstract.GmapServices;
using SW.HomeVisits.Application.GmapServices;
using SW.HomeVisits.Application.Abstract.Intergartions;
using SW.HomeVisits.Application.Integrations;
using SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits;
using SW.HomeVisits.Application.AssignAndOptimizeVisits;

namespace SW.HomeVisits.Application
{
    public class HomeVisitsApplicationModule
    {
        public void Initialize(IServiceCollection services)
        {
            RegisterCommandHandlers(services);
        }
        private void RegisterCommandHandlers(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<ICreateSystemParametersCommand>, CreateSystemParametersCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateSystemParemetersCommand>, UpdateSystemParemetersCommandHandler>();

            services.AddTransient<ICommandHandler<ICreatePatientAuthCommand>, CreatePatientAuthCommandHandler>();

            services.AddTransient<ICommandHandler<ICreateChemistCommand>, CreateChemistCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateChemistCommand>, UpdateChemistCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteChemistCommand>, DeleteChemistCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateRoleCommand>, CreateRoleCommandHandler>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<INotitificationService, NotificationService>();
            services.AddTransient<ICommandHandler<ICreateChemistTrackingLogCommand>, CreateChemistTrackingLogCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateVisitStatusCommand>, CreateVisitStatusCommandHandler>();
            services.AddTransient<ICommandHandler<IAddPatientAddressCommand>, AddPatientAddressCommandHandler>();
            services.AddTransient<ICommandHandler<IAddPatientPhoneCommand>, AddPatientPhoneCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateLostVisitTimeCommand>, CreateLostVisitTimeCommandHandler>();
            services.AddTransient<ICommandHandler<IAddVisitCommand>, AddVisitCommandHandler>();
            services.AddTransient<ICommandHandler<IAddOnHoldVisitCommand>, AddOnHoldVisitCommandHandler>();
            services.AddTransient<ICommandHandler<IAddSecondVisitCommand>, AddSecondVisitCommandHandler>();
            services.AddTransient<ICommandHandler<IAddUserDeviceCommand>, AddUserDeviceCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateRoleCommand>, UpdateRoleCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteRoleCommand>, DeleteRoleCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateClientUserCommand>, CreateClientUserCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateClientUserCommand>, UpdateClientUserCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteClientuserCommand>, DeleteClientUserCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateChemistScheduleCommand>, CreateChemistScheduleCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateReasonCommand>, CreateReasonCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateReasonCommand>, UpdateReasonCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteReasonCommand>, DeleteReasonCommandHandler>();
            services.AddTransient<ICommandHandler<IAddPatientCommand>, AddPatientCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdatePatientCommand>, UpdatePatientCommandHandler>();
            services.AddTransient<ICommandHandler<IDeletePatientAddressCommand>, DeletePatientAddressCommandHandler>();
            services.AddTransient<ICommandHandler<IDeletePatientPhoneCommand>, DeletePatientPhoneCommandHandler>();
            services.AddTransient<ICommandHandler<IDeletePatientCommand>, DeletePatientCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateChemistPermitCommand>, CreateChemistPermitCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateChemistPermitCommand>, UpdateChemistPermitCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteChemistPermitCommand>, DeleteChemistPermitCommandHandler>();
            services.AddTransient<IGmapService, GmapService>();
            services.AddTransient<IIntegrationManager, IntegrationManager>();
            services.AddTransient<ICommandHandler<ICreateCountryCommand>, CreateCountryCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateCountryCommand>, UpdateCountryCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteCountryCommand>, DeleteCountryCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteGovernatCommand>, DeleteGovernateCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateGovernateCommand>, CreateGovernateCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateGovernateCommand>, UpdateGovernateCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteGeoZoneCommand>, DeleteGeoZoneCommandHandler>();
            services.AddTransient<ICommandHandler<IAddGeoZoneCommand>, AddGeoZoneCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateGeoZoneCommand>, UpdateGeoZoneCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteAgeSegmentCommand>, DeleteAgeSegmentCommandHandler>();
            services.AddTransient<ICommandHandler<ICreateAgeSegmentCommand>, CreateAgeSegmentCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateAgeSegmentCommand>, UpdateAgeSegmentCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateChemistScheduleCommand>, UpdateChemistScheduleCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteChemistScheduleCommand>, DeleteChemistScheduleCommandHandler>();
            services.AddTransient<ICommandHandler<IDuplicateChemistScheduleCommand>, DuplicateChemistScheduleCommandHandler>();
            services.AddTransient<ICommandHandler<IAddClientCommand>, AddClientCommandHandler>();
            services.AddTransient<ICommandHandler<IAddChemistVisitOrderCommand>, AddChemistVisitOrderCommandHandler>();
            services.AddTransient<ICommandHandler<IUpdateClientCommand>, UpdateClientCommandHandler>();
            services.AddTransient<ICommandHandler<IDeleteClientCommand>, DeleteClientCommandHandler>();
            services.AddScoped<IAssignAndOptimizeVisitsService, AssignAndOptimizeVisitsService>();
            services.AddTransient<ICommandHandler<IUpdateClientUserPermissionCommand>, UpdateClientUserPermissionCommandHandler>();            
            services.AddTransient<ICommandHandler<ICreateVisitStatusByPatientCommand>, CreateVisitStatusByPatientCommandHandler>();
            services.AddTransient<ICommandHandler<IAddVisitByPatientCommand>, AddVisitByPatientCommandHandler>();
            //services.AddTransient<ICommandHandler<IAddPermissionCommand>, AddPermissionCommandHandler>();

            #region Register Security Rules

            services.RegisterDefaultAuthorizationManager();

            #endregion


            #region Register Validation Rules


            services.RegisterDefaultValidationManager();

            //Add all command handlers that you want to apply valdation rules on them
            services.Decorate<ICommandHandler<IAddPatientPhoneCommand>, ValidationCommandHandlerDecorator<IAddPatientPhoneCommand>>();
            services.Decorate<ICommandHandler<ICreateRoleCommand>, ValidationCommandHandlerDecorator<ICreateRoleCommand>>();
            services.Decorate<ICommandHandler<IUpdateRoleCommand>, ValidationCommandHandlerDecorator<IUpdateRoleCommand>>();
            services.Decorate<ICommandHandler<ICreateChemistCommand>, ValidationCommandHandlerDecorator<ICreateChemistCommand>>();
            services.AddTransient<IValidationRule<IAddPatientPhoneCommand>, CheckPatientPhoneExistsValidationRule>();
            services.AddTransient<IValidationRule<ICreateRoleCommand>, CheckRoleNameAlreadyExists>();
            services.AddTransient<IValidationRule<IUpdateRoleCommand>, CheckRoleNameAlreadyExists>();
            services.AddTransient<IValidationRule<ICreateChemistCommand>, ValidateBirthDateRule>();
            services.Decorate<ICommandHandler<ICreateCountryCommand>, ValidationCommandHandlerDecorator<ICreateCountryCommand>>();
            services.Decorate<ICommandHandler<IUpdateCountryCommand>, ValidationCommandHandlerDecorator<IUpdateCountryCommand>>();
            services.Decorate<ICommandHandler<ICreateGovernateCommand>, ValidationCommandHandlerDecorator<ICreateGovernateCommand>>();
            services.Decorate<ICommandHandler<IUpdateGovernateCommand>, ValidationCommandHandlerDecorator<IUpdateGovernateCommand>>();

            services.AddTransient<IValidationRule<IAddPatientPhoneCommand>, CheckPatientPhoneExistsValidationRule>();
            services.AddTransient<IValidationRule<ICreateRoleCommand>, CheckRoleNameAlreadyExists>();
            services.AddTransient<IValidationRule<IUpdateRoleCommand>, CheckRoleNameAlreadyExists>();
            services.AddTransient<IValidationRule<ICreateCountryCommand>, CheckCountryNameAlreadyExists>();
            services.AddTransient<IValidationRule<IUpdateCountryCommand>, CheckCountryNameAlreadyExists>();
            services.AddTransient<IValidationRule<ICreateGovernateCommand>, CheckGovernateNameAlreadyExists>();
            services.AddTransient<IValidationRule<IUpdateGovernateCommand>, CheckGovernateNameAlreadyExists>();
            #endregion
        }
    }
}