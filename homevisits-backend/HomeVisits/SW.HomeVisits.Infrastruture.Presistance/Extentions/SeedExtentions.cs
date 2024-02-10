using Microsoft.EntityFrameworkCore;
using SW.HomeVisits.Domain;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.Extentions
{
    public static class SeedExtentions
    {
        public static void Seed(HomeVisitsDomainContext context)
        {
            SeedSuperAdmin(context);
            SeedSystemPages(context);
            SeedPermissions(context);
            SeedSystemPagePermission(context);
            SeedDefualtRoles(context);
        }

        private static void SeedSuperAdmin(HomeVisitsDomainContext context)
        {
        }

        private static void SeedDefualtRoles(HomeVisitsDomainContext context)
        {
            var roles = context.Roles.ToList();
            Role clientAdminRole = Role.CreateNewRole(Guid.NewGuid(), null, Utility.ClientAdminRoleCode, "Client Admin Role", "Client Admin Role",
                "Client Admin Role", true, Guid.NewGuid(), 1);
            clientAdminRole.IsDeleted = false;
            clientAdminRole.IsStaticRole = true;
            if (roles.FirstOrDefault(p => p.Code == Utility.ClientAdminRoleCode) == null)
                context.Roles.Add(clientAdminRole);
            else
                clientAdminRole = roles.FirstOrDefault(p => p.Code == Utility.ClientAdminRoleCode);

            Role chemistRole = Role.CreateNewRole(Guid.NewGuid(), null, Utility.ChemistRoleCode, "Chemist Role", "Chemist Role",
                "Chemist Role", true, Guid.NewGuid(), 1);
            chemistRole.IsDeleted = false;
            chemistRole.IsStaticRole = true;
            if (roles.FirstOrDefault(p => p.Code == Utility.ChemistRoleCode) == null)
                context.Roles.Add(chemistRole);
            else
                chemistRole = roles.FirstOrDefault(p => p.Code == Utility.ChemistRoleCode);

            Role patientRole = Role.CreateNewRole(Guid.NewGuid(), null, Utility.PatientRoleCode, "Patient Role", "Patient Role",
                "Patient Role", true, Guid.NewGuid(), 1);
            patientRole.IsDeleted = false;
            patientRole.IsStaticRole = true;
            if (roles.FirstOrDefault(p => p.Code == Utility.PatientRoleCode) == null)
                context.Roles.Add(patientRole);
            else
                patientRole = roles.FirstOrDefault(p => p.Code == Utility.PatientRoleCode);

            context.SaveChanges();

            var systemPagePermissions = context.SystemPagePermissions.AsNoTracking().ToList();
            #region Client Admin Role Permission
            var clientAdminRolePermissions = context.RolePermissions.AsNoTracking().ToList();
            var newClientAdminRolePermissions = new List<RolePermission>();
            foreach (var item in systemPagePermissions)
            {
                if (!clientAdminRolePermissions.Any(p => p.SystemPagePermissionId == item.SystemPagePermissionId))
                    newClientAdminRolePermissions.Add(new RolePermission
                    {
                        IsDeleted = false,
                        RoleId = clientAdminRole.RoleId,
                        SystemPagePermissionId = item.SystemPagePermissionId
                    });
            }
            context.RolePermissions.AddRange(newClientAdminRolePermissions);
            context.SaveChanges();
            #endregion
            //#region Chemist Role Permission
            //var rolePermissions = context.RolePermissions.ToList();
            //var role109 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 109,
            //    RoleId = chemistRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role109.PermissionId && p.RoleId == role109.RoleId) == null)
            //    context.RolePermissions.Add(role109);

            //var role110 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 110,
            //    RoleId = chemistRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role110.PermissionId && p.RoleId == role110.RoleId) == null)
            //    context.RolePermissions.Add(role110);

            //var role111 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 111,
            //    RoleId = chemistRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role111.PermissionId && p.RoleId == role111.RoleId) == null)
            //    context.RolePermissions.Add(role111);

            //var role113 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 113,
            //    RoleId = chemistRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role113.PermissionId && p.RoleId == role113.RoleId) == null)
            //    context.RolePermissions.Add(role113);

            //var role116 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 116,
            //    RoleId = chemistRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role116.PermissionId && p.RoleId == role116.RoleId) == null)
            //    context.RolePermissions.Add(role116);
            //#endregion

            //#region Pataint Role Permission
            //var role81 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 81,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role81.PermissionId && p.RoleId == role81.RoleId) == null)
            //    context.RolePermissions.Add(role81);

            //var role82 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 82,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role82.PermissionId && p.RoleId == role82.RoleId) == null)
            //    context.RolePermissions.Add(role82);

            //var role83 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 83,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role83.PermissionId && p.RoleId == role83.RoleId) == null)
            //    context.RolePermissions.Add(role83);

            //var role84 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 84,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role84.PermissionId && p.RoleId == role84.RoleId) == null)
            //    context.RolePermissions.Add(role84);

            //var role85 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 85,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role85.PermissionId && p.RoleId == role85.RoleId) == null)
            //    context.RolePermissions.Add(role85);

            //var role86 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 86,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role86.PermissionId && p.RoleId == role86.RoleId) == null)
            //    context.RolePermissions.Add(role86);

            //var role87 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 87,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role87.PermissionId && p.RoleId == role87.RoleId) == null)
            //    context.RolePermissions.Add(role87);

            //var role88 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 88,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role88.PermissionId && p.RoleId == role88.RoleId) == null)
            //    context.RolePermissions.Add(role88);

            //var role89 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 89,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role89.PermissionId && p.RoleId == role89.RoleId) == null)
            //    context.RolePermissions.Add(role89);

            //var role90 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 90,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role90.PermissionId && p.RoleId == role90.RoleId) == null)
            //    context.RolePermissions.Add(role90);

            //var role91 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 91,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role91.PermissionId && p.RoleId == role91.RoleId) == null)
            //    context.RolePermissions.Add(role91);

            //var role92 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 92,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role92.PermissionId && p.RoleId == role92.RoleId) == null)
            //    context.RolePermissions.Add(role92);

            //var role125 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 125,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role125.PermissionId && p.RoleId == role125.RoleId) == null)
            //    context.RolePermissions.Add(role125);

            //var role128 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 128,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role128.PermissionId && p.RoleId == role128.RoleId) == null)
            //    context.RolePermissions.Add(role128);

            //var role131 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 131,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role131.PermissionId && p.RoleId == role131.RoleId) == null)
            //    context.RolePermissions.Add(role131);

            //var role135 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 135,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role135.PermissionId && p.RoleId == role135.RoleId) == null)
            //    context.RolePermissions.Add(role135);

            //var role140 = new RolePermission
            //{
            //    IsDeleted = false,
            //    PermissionId = 140,
            //    RoleId = patientRole.RoleId,
            //    RolePermissionId = Guid.NewGuid()
            //};
            //if (rolePermissions.FirstOrDefault(p => p.PermissionId == role140.PermissionId && p.RoleId == role140.RoleId) == null)
            //    context.RolePermissions.Add(role140);
            //#endregion

            context.SaveChanges();
        }

        private static void SeedSystemPagePermission(HomeVisitsDomainContext context)
        {
            var systemPagePermissions = context.SystemPagePermissions.AsNoTracking().ToList();

            #region Dachboard
            var viewDashboardPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 1,
                SystemPageId = (int)SystemPagesEnum.Dashboard,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewDashboardPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewDashboardPermission);
            else
                context.SystemPagePermissions.Update(viewDashboardPermission);
            #endregion

            #region System Configration Permission
            var viewSystemConfigratioPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 72,
                SystemPageId = (int)SystemPagesEnum.SystemConfiguration,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewSystemConfigratioPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewSystemConfigratioPermission);
            else
                context.SystemPagePermissions.Update(viewSystemConfigratioPermission);
            #endregion

            #region System Parameters
            var viewSystemParametersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 2,
                SystemPageId = (int)SystemPagesEnum.SystemParameters,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewSystemParametersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewSystemParametersPermission);
            else
                context.SystemPagePermissions.Update(viewSystemParametersPermission);

            var saveSystemParametersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 3,
                SystemPageId = (int)SystemPagesEnum.SystemParameters,
                PermissionId = (int)PermissionEnum.Create,
                NameAr = "Change System Parameters",
                NameEn = "Change System Parameters"
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == saveSystemParametersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(saveSystemParametersPermission);
            else
                context.SystemPagePermissions.Update(saveSystemParametersPermission);
            #endregion

            #region Countries Permissions
            var viewCountriesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 4,
                SystemPageId = (int)SystemPagesEnum.Countries,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewCountriesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewCountriesPermission);
            else
                context.SystemPagePermissions.Update(viewCountriesPermission);

            var createCountriesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 5,
                SystemPageId = (int)SystemPagesEnum.Countries,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createCountriesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createCountriesPermission);
            else
                context.SystemPagePermissions.Update(createCountriesPermission);

            var updateCountriesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 6,
                SystemPageId = (int)SystemPagesEnum.Countries,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateCountriesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateCountriesPermission);
            else
                context.SystemPagePermissions.Update(updateCountriesPermission);

            var deleteCountriesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 7,
                SystemPageId = (int)SystemPagesEnum.Countries,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteCountriesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteCountriesPermission);
            else
                context.SystemPagePermissions.Update(deleteCountriesPermission);
            #endregion

            #region Governorates Permissions
            var viewGovernoratesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 8,
                SystemPageId = (int)SystemPagesEnum.Governorates,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewGovernoratesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewGovernoratesPermission);
            else
                context.SystemPagePermissions.Update(viewGovernoratesPermission);

            var createGovernoratesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 9,
                SystemPageId = (int)SystemPagesEnum.Governorates,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createGovernoratesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createGovernoratesPermission);
            else
                context.SystemPagePermissions.Update(createGovernoratesPermission);

            var updateGovernoratesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 10,
                SystemPageId = (int)SystemPagesEnum.Governorates,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateGovernoratesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateGovernoratesPermission);
            else
                context.SystemPagePermissions.Update(updateGovernoratesPermission);

            var deleteGovernoratesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 11,
                SystemPageId = (int)SystemPagesEnum.Governorates,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteGovernoratesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteGovernoratesPermission);
            else
                context.SystemPagePermissions.Update(deleteGovernoratesPermission);
            #endregion

            #region Areas Permissions
            var viewAreasPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 12,
                SystemPageId = (int)SystemPagesEnum.Areas,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewAreasPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewAreasPermission);
            else
                context.SystemPagePermissions.Update(viewAreasPermission);

            var createAreasPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 13,
                SystemPageId = (int)SystemPagesEnum.Areas,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createAreasPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createAreasPermission);
            else
                context.SystemPagePermissions.Update(createAreasPermission);

            var updateAreasPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 14,
                SystemPageId = (int)SystemPagesEnum.Areas,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateAreasPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateAreasPermission);
            else
                context.SystemPagePermissions.Update(updateAreasPermission);

            var deleteAreasPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 15,
                SystemPageId = (int)SystemPagesEnum.Areas,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteAreasPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteAreasPermission);
            else
                context.SystemPagePermissions.Update(deleteAreasPermission);
            #endregion

            #region Request Second Visit Reasons Permissions
            var viewRequestSecondVisitReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 16,
                SystemPageId = (int)SystemPagesEnum.RequestSecondVisitReasons,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewRequestSecondVisitReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewRequestSecondVisitReasonsPermission);
            else
                context.SystemPagePermissions.Update(viewRequestSecondVisitReasonsPermission);

            var createRequestSecondVisitReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 17,
                SystemPageId = (int)SystemPagesEnum.RequestSecondVisitReasons,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createRequestSecondVisitReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createRequestSecondVisitReasonsPermission);
            else
                context.SystemPagePermissions.Update(createRequestSecondVisitReasonsPermission);

            var updateRequestSecondVisitReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 18,
                SystemPageId = (int)SystemPagesEnum.RequestSecondVisitReasons,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateRequestSecondVisitReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateRequestSecondVisitReasonsPermission);
            else
                context.SystemPagePermissions.Update(updateRequestSecondVisitReasonsPermission);

            var deleteRequestSecondVisitReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 19,
                SystemPageId = (int)SystemPagesEnum.RequestSecondVisitReasons,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteRequestSecondVisitReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteRequestSecondVisitReasonsPermission);
            else
                context.SystemPagePermissions.Update(deleteRequestSecondVisitReasonsPermission);
            #endregion

            #region ReAssign Reasons Permissions
            var viewReAssignReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 20,
                SystemPageId = (int)SystemPagesEnum.ReAssignReasons,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewReAssignReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewReAssignReasonsPermission);
            else
                context.SystemPagePermissions.Update(viewReAssignReasonsPermission);

            var createReAssignReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 21,
                SystemPageId = (int)SystemPagesEnum.ReAssignReasons,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createReAssignReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createReAssignReasonsPermission);
            else
                context.SystemPagePermissions.Update(createReAssignReasonsPermission);

            var updateReAssignReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 22,
                SystemPageId = (int)SystemPagesEnum.ReAssignReasons,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateReAssignReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateReAssignReasonsPermission);
            else
                context.SystemPagePermissions.Update(updateReAssignReasonsPermission);

            var deleteReAssignReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 23,
                SystemPageId = (int)SystemPagesEnum.ReAssignReasons,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteReAssignReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteReAssignReasonsPermission);
            else
                context.SystemPagePermissions.Update(deleteReAssignReasonsPermission);
            #endregion

            #region Cancellation Reasons Permissions
            var viewCancellationReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 24,
                SystemPageId = (int)SystemPagesEnum.CancellationReasons,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewCancellationReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewCancellationReasonsPermission);
            else
                context.SystemPagePermissions.Update(viewCancellationReasonsPermission);

            var createCancellationReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 25,
                SystemPageId = (int)SystemPagesEnum.CancellationReasons,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createCancellationReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createCancellationReasonsPermission);
            else
                context.SystemPagePermissions.Update(createCancellationReasonsPermission);

            var updateCancellationReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 26,
                SystemPageId = (int)SystemPagesEnum.CancellationReasons,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateCancellationReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateCancellationReasonsPermission);
            else
                context.SystemPagePermissions.Update(updateCancellationReasonsPermission);

            var deleteCancellationReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 27,
                SystemPageId = (int)SystemPagesEnum.CancellationReasons,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteCancellationReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteCancellationReasonsPermission);
            else
                context.SystemPagePermissions.Update(deleteCancellationReasonsPermission);
            #endregion

            #region Reject Reasons Permissions
            var viewRejectReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 28,
                SystemPageId = (int)SystemPagesEnum.RejectReasons,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewRejectReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewRejectReasonsPermission);
            else
                context.SystemPagePermissions.Update(viewRejectReasonsPermission);

            var createRejectReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 29,
                SystemPageId = (int)SystemPagesEnum.RejectReasons,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createRejectReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createRejectReasonsPermission);
            else
                context.SystemPagePermissions.Update(createRejectReasonsPermission);

            var updateRejectReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 30,
                SystemPageId = (int)SystemPagesEnum.RejectReasons,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateRejectReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateRejectReasonsPermission);
            else
                context.SystemPagePermissions.Update(updateRejectReasonsPermission);

            var deleteRejectReasonsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 31,
                SystemPageId = (int)SystemPagesEnum.RejectReasons,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteRejectReasonsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteRejectReasonsPermission);
            else
                context.SystemPagePermissions.Update(deleteRejectReasonsPermission);
            #endregion

            #region Age Segments Permissions
            var viewAgeSegmentsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 32,
                SystemPageId = (int)SystemPagesEnum.AgeSegments,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewAgeSegmentsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewAgeSegmentsPermission);
            else
                context.SystemPagePermissions.Update(viewAgeSegmentsPermission);

            var createAgeSegmentsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 33,
                SystemPageId = (int)SystemPagesEnum.AgeSegments,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createAgeSegmentsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createAgeSegmentsPermission);
            else
                context.SystemPagePermissions.Update(createAgeSegmentsPermission);

            var updateAgeSegmentsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 34,
                SystemPageId = (int)SystemPagesEnum.AgeSegments,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateAgeSegmentsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateAgeSegmentsPermission);
            else
                context.SystemPagePermissions.Update(updateAgeSegmentsPermission);

            var deleteAgeSegmentsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 35,
                SystemPageId = (int)SystemPagesEnum.AgeSegments,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteAgeSegmentsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteAgeSegmentsPermission);
            else
                context.SystemPagePermissions.Update(deleteAgeSegmentsPermission);
            #endregion

            #region Roles Permissions
            var viewRolesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 36,
                SystemPageId = (int)SystemPagesEnum.Roles,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewRolesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewRolesPermission);
            else
                context.SystemPagePermissions.Update(viewRolesPermission);

            var createRolesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 37,
                SystemPageId = (int)SystemPagesEnum.Roles,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createRolesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createRolesPermission);
            else
                context.SystemPagePermissions.Update(createRolesPermission);

            var updateRolesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 38,
                SystemPageId = (int)SystemPagesEnum.Roles,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateRolesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateRolesPermission);
            else
                context.SystemPagePermissions.Update(updateRolesPermission);

            var deleteRolesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 39,
                SystemPageId = (int)SystemPagesEnum.Roles,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteRolesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteRolesPermission);
            else
                context.SystemPagePermissions.Update(deleteRolesPermission);
            #endregion

            #region Users Permissions
            var viewUsersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 40,
                SystemPageId = (int)SystemPagesEnum.Users,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewUsersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewUsersPermission);
            else
                context.SystemPagePermissions.Update(viewUsersPermission);

            var createUsersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 41,
                SystemPageId = (int)SystemPagesEnum.Users,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createUsersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createUsersPermission);
            else
                context.SystemPagePermissions.Update(createUsersPermission);

            var updateUsersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 42,
                SystemPageId = (int)SystemPagesEnum.Users,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateUsersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateUsersPermission);
            else
                context.SystemPagePermissions.Update(updateUsersPermission);

            var deleteUsersPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 43,
                SystemPageId = (int)SystemPagesEnum.Users,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteUsersPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteUsersPermission);
            else
                context.SystemPagePermissions.Update(deleteUsersPermission);
            #endregion

            #region Add New Visit Permission
            var createNewVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 44,
                SystemPageId = (int)SystemPagesEnum.AddNewVisit,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createNewVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createNewVisitPermission);
            else
                context.SystemPagePermissions.Update(createNewVisitPermission);
            #endregion

            #region View Visit Permissions
            var viewVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 45,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewVisitPermission);
            else
                context.SystemPagePermissions.Update(viewVisitPermission);

            var createVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 46,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createVisitPermission);
            else
                context.SystemPagePermissions.Update(createVisitPermission);

            var requestSecondVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 47,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.RequestSecondVisit
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == requestSecondVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(requestSecondVisitPermission);
            else
                context.SystemPagePermissions.Update(requestSecondVisitPermission);

            var reassignChimistVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 48,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.ReassignChimist,
                NameAr = "Re-assign Visit",
                NameEn = "Re-assign Visit"
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == reassignChimistVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(reassignChimistVisitPermission);
            else
                context.SystemPagePermissions.Update(reassignChimistVisitPermission);

            var cancelVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 49,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.Cancel,
                NameAr = "Cancel Visit",
                NameEn = "Cancel Visit"
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == cancelVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(cancelVisitPermission);
            else
                context.SystemPagePermissions.Update(cancelVisitPermission);

            var approveVisitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 50,
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                PermissionId = (int)PermissionEnum.Approve,
                NameAr = "Approve Visit",
                NameEn = "Approve Visit"
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == approveVisitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(approveVisitPermission);
            else
                context.SystemPagePermissions.Update(approveVisitPermission);
            #endregion

            #region Add New Chemist Permission
            var createNewChemistPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 51,
                SystemPageId = (int)SystemPagesEnum.AddNewChemists,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createNewChemistPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createNewChemistPermission);
            else
                context.SystemPagePermissions.Update(createNewChemistPermission);
            #endregion

            #region Chemist Views Permissions
            var viewChemistsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 52,
                SystemPageId = (int)SystemPagesEnum.ViewChemists,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewChemistsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewChemistsPermission);
            else
                context.SystemPagePermissions.Update(viewChemistsPermission);

            var createChemistsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 53,
                SystemPageId = (int)SystemPagesEnum.ViewChemists,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createChemistsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createChemistsPermission);
            else
                context.SystemPagePermissions.Update(createChemistsPermission);

            var updateChemistsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 54,
                SystemPageId = (int)SystemPagesEnum.ViewChemists,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateChemistsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateChemistsPermission);
            else
                context.SystemPagePermissions.Update(updateChemistsPermission);

            var deleteChemistsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 55,
                SystemPageId = (int)SystemPagesEnum.ViewChemists,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteChemistsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteChemistsPermission);
            else
                context.SystemPagePermissions.Update(deleteChemistsPermission);
            #endregion

            #region Chemist Schedule Permissions
            var viewChemistSchedulePermission = new SystemPagePermission
            {
                SystemPagePermissionId = 56,
                SystemPageId = (int)SystemPagesEnum.ChemistSchedule,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewChemistSchedulePermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewChemistSchedulePermission);
            else
                context.SystemPagePermissions.Update(viewChemistSchedulePermission);

            var createChemistSchedulePermission = new SystemPagePermission
            {
                SystemPagePermissionId = 57,
                SystemPageId = (int)SystemPagesEnum.ChemistSchedule,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createChemistSchedulePermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createChemistSchedulePermission);
            else
                context.SystemPagePermissions.Update(createChemistSchedulePermission);

            var updateChemistSchedulePermission = new SystemPagePermission
            {
                SystemPagePermissionId = 58,
                SystemPageId = (int)SystemPagesEnum.ChemistSchedule,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateChemistSchedulePermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateChemistSchedulePermission);
            else
                context.SystemPagePermissions.Update(updateChemistSchedulePermission);

            var deleteChemistSchedulePermission = new SystemPagePermission
            {
                SystemPagePermissionId = 59,
                SystemPageId = (int)SystemPagesEnum.ChemistSchedule,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteChemistSchedulePermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteChemistSchedulePermission);
            else
                context.SystemPagePermissions.Update(deleteChemistSchedulePermission);
            #endregion

            #region Track Chemists Permission
            var viewTrackChemistsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 60,
                SystemPageId = (int)SystemPagesEnum.TrackChemists,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewTrackChemistsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewTrackChemistsPermission);
            else
                context.SystemPagePermissions.Update(viewTrackChemistsPermission);
            #endregion

            #region Query Time Permission
            var viewQueryTimePermission = new SystemPagePermission
            {
                SystemPagePermissionId = 61,
                SystemPageId = (int)SystemPagesEnum.QueryTime,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewQueryTimePermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewQueryTimePermission);
            else
                context.SystemPagePermissions.Update(viewQueryTimePermission);
            #endregion

            #region Terms And Policies Permission
            var viewTermsAndPoliciesPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 62,
                SystemPageId = (int)SystemPagesEnum.TermsAndPolicies,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewTermsAndPoliciesPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewTermsAndPoliciesPermission);
            else
                context.SystemPagePermissions.Update(viewTermsAndPoliciesPermission);
            #endregion

            #region Patient Views Permissions
            var viewPatientPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 63,
                SystemPageId = (int)SystemPagesEnum.Patient,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewPatientPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewPatientPermission);
            else
                context.SystemPagePermissions.Update(viewPatientPermission);

            var createPatientPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 64,
                SystemPageId = (int)SystemPagesEnum.Patient,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createPatientPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createPatientPermission);
            else
                context.SystemPagePermissions.Update(createPatientPermission);

            var updatePatientPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 65,
                SystemPageId = (int)SystemPagesEnum.Patient,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updatePatientPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updatePatientPermission);
            else
                context.SystemPagePermissions.Update(updatePatientPermission);
            #endregion

            #region Visit Reports Permission
            var viewVisitReportsPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 66,
                SystemPageId = (int)SystemPagesEnum.VisitReports,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewVisitReportsPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewVisitReportsPermission);
            else
                context.SystemPagePermissions.Update(viewVisitReportsPermission);
            #endregion

            #region TAT Tracking Permission
            var viewTATTrackingPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 67,
                SystemPageId = (int)SystemPagesEnum.TATTracking,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewTATTrackingPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewTATTrackingPermission);
            else
                context.SystemPagePermissions.Update(viewTATTrackingPermission);
            #endregion

            #region ReAssigned Reports Permission
            var viewReAssignedReportPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 68,
                SystemPageId = (int)SystemPagesEnum.ReAssignedReport,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewReAssignedReportPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewReAssignedReportPermission);
            else
                context.SystemPagePermissions.Update(viewReAssignedReportPermission);
            #endregion

            #region Rejected Report Permission
            var viewRejectedReportPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 69,
                SystemPageId = (int)SystemPagesEnum.RejectedReport,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewRejectedReportPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewRejectedReportPermission);
            else
                context.SystemPagePermissions.Update(viewRejectedReportPermission);
            #endregion

            #region Lost Business Report Permission
            var viewLostBusinessReportPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 70,
                SystemPageId = (int)SystemPagesEnum.LostBusinessReport,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewLostBusinessReportPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewLostBusinessReportPermission);
            else
                context.SystemPagePermissions.Update(viewLostBusinessReportPermission);
            #endregion

            #region Cancelled Visit Report Permission
            var viewCancelledVisitReportPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 71,
                SystemPageId = (int)SystemPagesEnum.CancelledVisitReport,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewCancelledVisitReportPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewCancelledVisitReportPermission);
            else
                context.SystemPagePermissions.Update(viewCancelledVisitReportPermission);
            #endregion

            #region Chemist Schedule Permissions
            var viewChemistPermitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 73,
                SystemPageId = (int)SystemPagesEnum.ChemistPermit,
                PermissionId = (int)PermissionEnum.View
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == viewChemistPermitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(viewChemistPermitPermission);
            else
                context.SystemPagePermissions.Update(viewChemistPermitPermission);

            var createChemistPermitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 74,
                SystemPageId = (int)SystemPagesEnum.ChemistPermit,
                PermissionId = (int)PermissionEnum.Create
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == createChemistPermitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(createChemistPermitPermission);
            else
                context.SystemPagePermissions.Update(createChemistPermitPermission);

            var updateChemistPermitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 75,
                SystemPageId = (int)SystemPagesEnum.ChemistPermit,
                PermissionId = (int)PermissionEnum.Update
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == updateChemistPermitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(updateChemistPermitPermission);
            else
                context.SystemPagePermissions.Update(updateChemistPermitPermission);

            var deleteChemistPermitPermission = new SystemPagePermission
            {
                SystemPagePermissionId = 76,
                SystemPageId = (int)SystemPagesEnum.ChemistPermit,
                PermissionId = (int)PermissionEnum.Delete
            };
            if (systemPagePermissions.FirstOrDefault(p => p.SystemPagePermissionId == deleteChemistPermitPermission.SystemPagePermissionId) == null)
                context.SystemPagePermissions.Add(deleteChemistPermitPermission);
            else
                context.SystemPagePermissions.Update(deleteChemistPermitPermission);
            #endregion

            context.SaveChanges();
        }

        private static void SeedSystemPages(HomeVisitsDomainContext context)
        {
            var systemPages = context.SystemPages.AsNoTracking().ToList();
            var dashboard = new SystemPage
            {
                Code = "1",
                Position = 1,
                SystemPageId = (int)SystemPagesEnum.Dashboard,
                NameAr = "Dashboard",
                NameEn = "Dashboard",
                HasURL = true,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == dashboard.SystemPageId) == null)
                context.SystemPages.Add(dashboard);
            else
                context.SystemPages.Update(dashboard);

            var systemConfiguration = new SystemPage
            {
                Code = "2",
                Position = 2,
                SystemPageId = (int)SystemPagesEnum.SystemConfiguration,
                NameAr = "System Configuration",
                NameEn = "System Configuration",
                HasURL = true,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == systemConfiguration.SystemPageId) == null)
                context.SystemPages.Add(systemConfiguration);
            else
                context.SystemPages.Update(systemConfiguration);

            var systemParameters = new SystemPage
            {
                Code = "3",
                Position = 1,
                SystemPageId = (int)SystemPagesEnum.SystemParameters,
                NameAr = "System Parameters",
                NameEn = "System Parameters",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == systemParameters.SystemPageId) == null)
                context.SystemPages.Add(systemParameters);
            else
                context.SystemPages.Update(systemParameters);

            var countries = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Countries,
                Position = 2,
                Code = "4",
                NameAr = "Countries",
                NameEn = "Countries",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == countries.SystemPageId) == null)
                context.SystemPages.Add(countries);
            else
                context.SystemPages.Update(countries);

            var governorates = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Governorates,
                Position = 3,
                Code = "5",
                NameAr = "Governorates",
                NameEn = "Governorates",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == governorates.SystemPageId) == null)
                context.SystemPages.Add(governorates);
            else
                context.SystemPages.Update(governorates);

            var areas = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Areas,
                Position = 4,
                Code = "6",
                NameAr = "Areas",
                NameEn = "Areas",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == areas.SystemPageId) == null)
                context.SystemPages.Add(areas);
            else
                context.SystemPages.Update(areas);

            var requestSecondVisitReasons = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.RequestSecondVisitReasons,
                Position = 5,
                Code = "7",
                NameAr = "Request Second Visit Reasons",
                NameEn = "Request Second Visit Reasons",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == requestSecondVisitReasons.SystemPageId) == null)
                context.SystemPages.Add(requestSecondVisitReasons);
            else
                context.SystemPages.Update(requestSecondVisitReasons);

            var reAssignReasons = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ReAssignReasons,
                Position = 6,
                Code = "8",
                NameAr = "Re-Assign Reasons",
                NameEn = "Re-Assign Reasons",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == reAssignReasons.SystemPageId) == null)
                context.SystemPages.Add(reAssignReasons);
            else
                context.SystemPages.Update(reAssignReasons);

            var cancellationReasons = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.CancellationReasons,
                Position = 7,
                Code = "9",
                NameAr = "Cancellation Reasons",
                NameEn = "Cancellation Reasons",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == cancellationReasons.SystemPageId) == null)
                context.SystemPages.Add(cancellationReasons);
            else
                context.SystemPages.Update(cancellationReasons);

            var rejectReasons = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.RejectReasons,
                Position = 8,
                Code = "10",
                NameAr = "Reject Reasons",
                NameEn = "Reject Reasons",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == rejectReasons.SystemPageId) == null)
                context.SystemPages.Add(rejectReasons);
            else
                context.SystemPages.Update(rejectReasons);

            var ageSegments = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.AgeSegments,
                Position = 9,
                Code = "11",
                NameAr = "Age Segments",
                NameEn = "Age Segments",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == ageSegments.SystemPageId) == null)
                context.SystemPages.Add(ageSegments);
            else
                context.SystemPages.Update(ageSegments);

            var userManagement = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.UserManagement,
                Position = 3,
                Code = "12",
                NameAr = "User Management",
                NameEn = "User Management",
                HasURL = false,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == userManagement.SystemPageId) == null)
                context.SystemPages.Add(userManagement);
            else
                context.SystemPages.Update(userManagement);

            var roles = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Roles,
                Position = 1,
                Code = "13",
                NameAr = "Roles",
                NameEn = "Roles",
                HasURL = true,
                ParentId = 12,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == roles.SystemPageId) == null)
                context.SystemPages.Add(roles);
            else
                context.SystemPages.Update(roles);

            var users = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Users,
                Position = 2,
                Code = "14",
                NameAr = "Users",
                NameEn = "Users",
                HasURL = true,
                ParentId = 12,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == users.SystemPageId) == null)
                context.SystemPages.Add(users);
            else
                context.SystemPages.Update(users);

            var homeVisit = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.HomeVisit,
                Position = 4,
                Code = "15",
                NameAr = "Home Visit",
                NameEn = "Home Visit",
                HasURL = false,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == homeVisit.SystemPageId) == null)
                context.SystemPages.Add(homeVisit);
            else
                context.SystemPages.Update(homeVisit);

            var addNewVisit = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.AddNewVisit,
                Position = 1,
                Code = "16",
                NameAr = "Add New Visit",
                NameEn = "Add New Visit",
                HasURL = true,
                ParentId = 15,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == addNewVisit.SystemPageId) == null)
                context.SystemPages.Add(addNewVisit);
            else
                context.SystemPages.Update(addNewVisit);

            var viewVisit = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ViewVisit,
                Position = 2,
                Code = "17",
                NameAr = "View Visit",
                NameEn = "View Visit",
                HasURL = true,
                ParentId = 15,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == viewVisit.SystemPageId) == null)
                context.SystemPages.Add(viewVisit);
            else
                context.SystemPages.Update(viewVisit);

            var queryTime = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.QueryTime,
                Position = 3,
                Code = "28",
                NameAr = "Query Time",
                NameEn = "Query Time",
                HasURL = true,
                ParentId = 15,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == queryTime.SystemPageId) == null)
                context.SystemPages.Add(queryTime);
            else
                context.SystemPages.Update(queryTime);

            var chemists = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Chemists,
                Position = 5,
                Code = "18",
                NameAr = "Chemists",
                NameEn = "Chemists",
                HasURL = false,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == chemists.SystemPageId) == null)
                context.SystemPages.Add(chemists);
            else
                context.SystemPages.Update(chemists);

            var addChemists = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.AddNewChemists,
                Position = 1,
                Code = "19",
                NameAr = "Add New Chemists",
                NameEn = "Add New Chemists",
                HasURL = true,
                ParentId = 18,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == addChemists.SystemPageId) == null)
                context.SystemPages.Add(addChemists);
            else
                context.SystemPages.Update(addChemists);

            var viewChemists = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ViewChemists,
                Position = 2,
                Code = "20",
                NameAr = "View Chemists",
                NameEn = "View Chemists",
                HasURL = true,
                ParentId = 18,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == viewChemists.SystemPageId) == null)
                context.SystemPages.Add(viewChemists);
            else
                context.SystemPages.Update(viewChemists);

            var trackChemists = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.TrackChemists,
                Position = 3,
                Code = "29",
                NameAr = "Track Chemists",
                NameEn = "Track Chemists",
                HasURL = true,
                ParentId = 18,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == trackChemists.SystemPageId) == null)
                context.SystemPages.Add(trackChemists);
            else
                context.SystemPages.Update(trackChemists);

            var chemistSchedule = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ChemistSchedule,
                Position = 4,
                Code = "31",
                NameAr = "Chemist Schedule",
                NameEn = "Chemist Schedule",
                HasURL = true,
                ParentId = 18,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == chemistSchedule.SystemPageId) == null)
                context.SystemPages.Add(chemistSchedule);
            else
                context.SystemPages.Update(chemistSchedule);

            var patient = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Patient,
                Position = 6,
                Code = "30",
                NameAr = "Patient",
                NameEn = "Patient",
                HasURL = true,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == patient.SystemPageId) == null)
                context.SystemPages.Add(patient);
            else
                context.SystemPages.Update(patient);

            var reports = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.Reports,
                Position = 7,
                Code = "21",
                NameAr = "Reports",
                NameEn = "Reports",
                HasURL = false,
                ParentId = null,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == reports.SystemPageId) == null)
                context.SystemPages.Add(reports);
            else
                context.SystemPages.Update(reports);

            var visitReports = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.VisitReports,
                Position = 1,
                Code = "22",
                NameAr = "Visit Reports",
                NameEn = "Visit Reports",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == visitReports.SystemPageId) == null)
                context.SystemPages.Add(visitReports);
            else
                context.SystemPages.Update(visitReports);

            var tatTracking = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.TATTracking,
                Position = 2,
                Code = "23",
                NameAr = "TAT Tracking",
                NameEn = "TAT Tracking",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == tatTracking.SystemPageId) == null)
                context.SystemPages.Add(tatTracking);
            else
                context.SystemPages.Update(tatTracking);

            var reassignedReport = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ReAssignedReport,
                Position = 3,
                Code = "24",
                NameAr = "ReAssigned Report",
                NameEn = "ReAssigned Report",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == reassignedReport.SystemPageId) == null)
                context.SystemPages.Add(reassignedReport);
            else
                context.SystemPages.Update(reassignedReport);

            var rejectedReport = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.RejectedReport,
                Position = 4,
                Code = "25",
                NameAr = "Rejected Report",
                NameEn = "Rejected Report",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == rejectedReport.SystemPageId) == null)
                context.SystemPages.Add(rejectedReport);
            else
                context.SystemPages.Update(rejectedReport);

            var lostBusinessReport = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.LostBusinessReport,
                Position = 5,
                Code = "26",
                NameAr = "Lost Business Report",
                NameEn = "Lost Business Report",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == lostBusinessReport.SystemPageId) == null)
                context.SystemPages.Add(lostBusinessReport);
            else
                context.SystemPages.Update(lostBusinessReport);

            var cancelledVisitReport = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.CancelledVisitReport,
                Position = 6,
                Code = "27",
                NameAr = "Cancelled Visit Report",
                NameEn = "Cancelled Visit Report",
                HasURL = true,
                ParentId = 21,
                IsDisplayInMenue = true
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == cancelledVisitReport.SystemPageId) == null)
                context.SystemPages.Add(cancelledVisitReport);
            else
                context.SystemPages.Update(cancelledVisitReport);

            var termsAndPoliciesReport = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.TermsAndPolicies,
                Position = 10,
                Code = "32",
                NameAr = "Terms And Policies",
                NameEn = "Terms And Policies",
                HasURL = true,
                ParentId = 2,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == termsAndPoliciesReport.SystemPageId) == null)
                context.SystemPages.Add(termsAndPoliciesReport);
            else
                context.SystemPages.Update(termsAndPoliciesReport);

            var chemistPermit = new SystemPage
            {
                SystemPageId = (int)SystemPagesEnum.ChemistPermit,
                Position = 11,
                Code = "33",
                NameAr = "Chemist Permit",
                NameEn = "Chemist Permit",
                HasURL = true,
                ParentId = 18,
                IsDisplayInMenue = false
            };
            if (systemPages.FirstOrDefault(p => p.SystemPageId == chemistPermit.SystemPageId) == null)
                context.SystemPages.Add(chemistPermit);
            else
                context.SystemPages.Update(chemistPermit);

            context.SaveChanges();
        }

        private static void SeedPermissions(HomeVisitsDomainContext context)
        {
            var permissions = context.Permissions.AsNoTracking().ToList();
            var viewPermission = new Permission
            {
                PermissionId = 1,
                Code = 1,
                IsActive = true,
                NameAr = "View",
                NameEn = "View",
                Position = 1
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == viewPermission.PermissionId) == null)
                context.Permissions.Add(viewPermission);
            else
                context.Permissions.Update(viewPermission);

            var createPermission = new Permission
            {
                PermissionId = 2,
                Code = 2,
                IsActive = true,
                NameAr = "Create",
                NameEn = "Create",
                Position = 2
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == createPermission.PermissionId) == null)
                context.Permissions.Add(createPermission);
            else
                context.Permissions.Update(createPermission);

            var updatePermission = new Permission
            {
                PermissionId = 3,
                Code = 3,
                IsActive = true,
                NameAr = "Update",
                NameEn = "Update",
                Position = 3
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == updatePermission.PermissionId) == null)
                context.Permissions.Add(updatePermission);
            else
                context.Permissions.Update(updatePermission);

            var deletePermission = new Permission
            {
                PermissionId = 4,
                Code = 4,
                IsActive = true,
                NameAr = "Delete",
                NameEn = "Delete",
                Position = 4
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == deletePermission.PermissionId) == null)
                context.Permissions.Add(deletePermission);
            else
                context.Permissions.Update(deletePermission);

            var approvePermission = new Permission
            {
                PermissionId = 5,
                Code = 5,
                IsActive = true,
                NameAr = "Approve",
                NameEn = "Approve",
                Position = 5
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == approvePermission.PermissionId) == null)
                context.Permissions.Add(approvePermission);
            else
                context.Permissions.Update(approvePermission);

            var cancelPermission = new Permission
            {
                PermissionId = 6,
                Code = 6,
                IsActive = true,
                NameAr = "Cancel",
                NameEn = "Cancel",
                Position = 6
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == cancelPermission.PermissionId) == null)
                context.Permissions.Add(cancelPermission);
            else
                context.Permissions.Update(cancelPermission);

            var requestSecondVisitPermission = new Permission
            {
                PermissionId = 7,
                Code = 7,
                IsActive = true,
                NameAr = "Request Second Visit",
                NameEn = "Request Second Visit",
                Position = 7
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == requestSecondVisitPermission.PermissionId) == null)
                context.Permissions.Add(requestSecondVisitPermission);
            else
                context.Permissions.Update(requestSecondVisitPermission);

            var reassignChimistPermission = new Permission
            {
                PermissionId = 8,
                Code = 8,
                IsActive = true,
                NameAr = "Reassign Chimist",
                NameEn = "Reassign Chimist",
                Position = 8
            };
            if (permissions.FirstOrDefault(p => p.PermissionId == reassignChimistPermission.PermissionId) == null)
                context.Permissions.Add(reassignChimistPermission);
            else
                context.Permissions.Update(reassignChimistPermission);

            context.SaveChanges();
        }

        //private static void SeedPermissions(HomeVisitsDomainContext context)
        //{
        //    var permissions = context.Permissions.ToList();

        //    #region Dashboard
        //    var getAreasByUserId = new Permission
        //    {
        //        Code = 173,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 73,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View User Areas",
        //        NameEn = "View User Areas",
        //        ControllerName = "HomePage",
        //        ActionName = "GetAreasByUserId",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAreasByUserId".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAreasByUserId);

        //    var getTotalVisitsList = new Permission
        //    {
        //        Code = 174,
        //        Position = 2,
        //        SystemPageId = 1,
        //        PermissionId = 74,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Total Visits",
        //        NameEn = "Total Visits",
        //        ControllerName = "HomePage",
        //        ActionName = "GetTotalVisitsList",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetTotalVisitsList".Trim().ToLower()) == null)
        //        context.Permissions.Add(getTotalVisitsList);

        //    var GetDelayedVisitsList = new Permission
        //    {
        //        Code = 175,
        //        Position = 3,
        //        SystemPageId = 1,
        //        PermissionId = 75,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Delayed Visits",
        //        NameEn = "View Delayed Visits",
        //        ControllerName = "HomePage",
        //        ActionName = "GetDelayedVisitsList",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetDelayedVisitsList".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetDelayedVisitsList);

        //    var getPendingVisitsList = new Permission
        //    {
        //        Code = 176,
        //        Position = 4,
        //        SystemPageId = 1,
        //        PermissionId = 76,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Pending Visits",
        //        NameEn = "View Pending Visits",
        //        ControllerName = "HomePage",
        //        ActionName = "GetPendingVisitsList",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetPendingVisitsList".Trim().ToLower()) == null)
        //        context.Permissions.Add(getPendingVisitsList);

        //    var getReassignedVisitsList = new Permission
        //    {
        //        Code = 177,
        //        Position = 5,
        //        SystemPageId = 1,
        //        PermissionId = 77,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Reassigned Visits",
        //        NameEn = "View Reassigned Visits",
        //        ControllerName = "HomePage",
        //        ActionName = "GetReassignedVisitsList",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetReassignedVisitsList".Trim().ToLower()) == null)
        //        context.Permissions.Add(getReassignedVisitsList);

        //    var getActiveChemists = new Permission
        //    {
        //        Code = 178,
        //        Position = 6,
        //        SystemPageId = 1,
        //        PermissionId = 78,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Active Chemists",
        //        NameEn = "View Active Chemists",
        //        ControllerName = "HomePage",
        //        ActionName = "GetActiveChemists",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetActiveChemists".Trim().ToLower()) == null)
        //        context.Permissions.Add(getActiveChemists);

        //    var getIdleChemists = new Permission
        //    {
        //        Code = 179,
        //        Position = 7,
        //        SystemPageId = 1,
        //        PermissionId = 79,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Idle Chemists",
        //        NameEn = "View Idle Chemists",
        //        ControllerName = "HomePage",
        //        ActionName = "GetIdleChemists",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetIdleChemists".Trim().ToLower()) == null)
        //        context.Permissions.Add(getIdleChemists);

        //    var getHomePageStatistics = new Permission
        //    {
        //        Code = 180,
        //        Position = 8,
        //        SystemPageId = 1,
        //        PermissionId = 80,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Home Page Statistics",
        //        NameEn = "View Home Page Statistics",
        //        ControllerName = "HomePage",
        //        ActionName = "GetHomePageStatistics",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "HomePage".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetHomePageStatistics".Trim().ToLower()) == null)
        //        context.Permissions.Add(getHomePageStatistics);
        //    #endregion

        //    #region System Parameters Permissions
        //    var getSystemParameterByClientId = new Permission
        //    {
        //        Code = 101,
        //        Position = 1,
        //        SystemPageId = 2,
        //        PermissionId = 1,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View System Parameters",
        //        NameEn = "View System Parameters",
        //        ControllerName = "SystemParameters",
        //        ActionName = "GetSystemParameterByClientId",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetSystemParameterByClientId".Trim().ToLower()) == null)
        //        context.Permissions.Add(getSystemParameterByClientId);

        //    var createSystemParemeters = new Permission
        //    {
        //        Code = 102,
        //        Position = 2,
        //        SystemPageId = 2,
        //        PermissionId = 2,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create System Parameters",
        //        NameEn = "Create System Parameters",
        //        ControllerName = "SystemParameters",
        //        ActionName = "CreateSystemParemeters",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateSystemParemeters".Trim().ToLower()) == null)
        //        context.Permissions.Add(createSystemParemeters);

        //    var updateSystemParemeters = new Permission
        //    {
        //        Code = 103,
        //        Position = 3,
        //        SystemPageId = 2,
        //        PermissionId = 3,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update System Parameters",
        //        NameEn = "Update System Parameters",
        //        ControllerName = "SystemParameters",
        //        ActionName = "UpdateSystemParemeters",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateSystemParemeters".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateSystemParemeters);

        //    var uploadPrecautionsFile = new Permission
        //    {
        //        Code = 104,
        //        Position = 4,
        //        SystemPageId = 2,
        //        PermissionId = 4,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Upload Precautions File",
        //        NameEn = "Upload Precautions File",
        //        ControllerName = "SystemParameters",
        //        ActionName = "UploadPrecautionsFile",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UploadPrecautionsFile".Trim().ToLower()) == null)
        //        context.Permissions.Add(uploadPrecautionsFile);

        //    var GetVisitAcceptCancelPermission = new Permission
        //    {
        //        Code = 222,
        //        Position = 5,
        //        SystemPageId = 2,
        //        PermissionId = 122,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Visit Accept Cancel Permission",
        //        NameEn = "Get Visit Accept Cancel Permission",
        //        ControllerName = "SystemParameters",
        //        ActionName = "GetVisitAcceptCancelPermission",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetVisitAcceptCancelPermission".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetVisitAcceptCancelPermission);

        //    var GetVisitAcceptAndCancelPermission = new Permission
        //    {
        //        Code = 223,
        //        Position = 6,
        //        SystemPageId = 2,
        //        PermissionId = 123,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Visit Accept And Cancel Permission",
        //        NameEn = "Get Visit Accept And Cancel Permission",
        //        ControllerName = "SystemParameters",
        //        ActionName = "GetVisitAcceptAndCancelPermission",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetVisitAcceptAndCancelPermission".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetVisitAcceptAndCancelPermission);

        //    var GetVisitDatesRegardingSysParam = new Permission
        //    {
        //        Code = 224,
        //        Position = 7,
        //        SystemPageId = 2,
        //        PermissionId = 124,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Visit Dates Regarding SysParam",
        //        NameEn = "Get Visit Dates Regarding SysParam",
        //        ControllerName = "SystemParameters",
        //        ActionName = "GetVisitDatesRegardingSysParam",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemParameters".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetVisitDatesRegardingSysParam".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetVisitDatesRegardingSysParam);
        //    #endregion

        //    #region Countries Permission
        //    var searchCountries = new Permission
        //    {
        //        Code = 105,
        //        Position = 1,
        //        SystemPageId = 3,
        //        PermissionId = 5,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Countries",
        //        NameEn = "View Countries",
        //        ControllerName = "Countries",
        //        ActionName = "SearchCountries",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchCountries".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchCountries);

        //    var createCountry = new Permission
        //    {
        //        Code = 106,
        //        Position = 2,
        //        SystemPageId = 3,
        //        PermissionId = 6,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Country",
        //        NameEn = "Create Country",
        //        ControllerName = "Countries",
        //        ActionName = "CreateCountry",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateCountry".Trim().ToLower()) == null)
        //        context.Permissions.Add(createCountry);

        //    var updateCountry = new Permission
        //    {
        //        Code = 107,
        //        Position = 3,
        //        SystemPageId = 3,
        //        PermissionId = 7,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Country",
        //        NameEn = "Update Country",
        //        ControllerName = "Countries",
        //        ActionName = "UpdateCountry",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateCountry".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateCountry);

        //    var deleteCountry = new Permission
        //    {
        //        Code = 108,
        //        Position = 4,
        //        SystemPageId = 3,
        //        PermissionId = 8,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Country",
        //        NameEn = "Delete Country",
        //        ControllerName = "Countries",
        //        ActionName = "DeleteCountry",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteCountry".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteCountry);

        //    var getCountryForEdit = new Permission
        //    {
        //        Code = 109,
        //        Position = 5,
        //        SystemPageId = 3,
        //        PermissionId = 9,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Country For Edit",
        //        NameEn = "Get Country For Edit",
        //        ControllerName = "Countries",
        //        ActionName = "GetCountryForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetCountryForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getCountryForEdit);
        //    #endregion

        //    #region Governats Permissions
        //    var searchGovernats = new Permission
        //    {
        //        Code = 110,
        //        Position = 1,
        //        SystemPageId = 4,
        //        PermissionId = 10,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Governats",
        //        NameEn = "View Governats",
        //        ControllerName = "Governats",
        //        ActionName = "SearchGovernats",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchGovernats".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchGovernats);

        //    var createGovernate = new Permission
        //    {
        //        Code = 111,
        //        Position = 2,
        //        SystemPageId = 4,
        //        PermissionId = 11,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Governat",
        //        NameEn = "Create Governat",
        //        ControllerName = "Governats",
        //        ActionName = "CreateGovernate",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateGovernate".Trim().ToLower()) == null)
        //        context.Permissions.Add(createGovernate);

        //    var updateGovernate = new Permission
        //    {
        //        Code = 112,
        //        Position = 3,
        //        SystemPageId = 4,
        //        PermissionId = 12,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Governat",
        //        NameEn = "Update Governat",
        //        ControllerName = "Governats",
        //        ActionName = "UpdateGovernate",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateGovernate".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateGovernate);

        //    var deleteGovernate = new Permission
        //    {
        //        Code = 113,
        //        Position = 4,
        //        SystemPageId = 4,
        //        PermissionId = 13,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Governate",
        //        NameEn = "Delete Governate",
        //        ControllerName = "Governats",
        //        ActionName = "DeleteGovernate",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteGovernate".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteGovernate);

        //    var getGovernateForEdit = new Permission
        //    {
        //        Code = 114,
        //        Position = 5,
        //        SystemPageId = 4,
        //        PermissionId = 14,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Governate For Edit",
        //        NameEn = "Get Governate For Edit",
        //        ControllerName = "Governats",
        //        ActionName = "GetGovernateForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetGovernateForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getGovernateForEdit);

        //    var GovernatsKeyValue = new Permission
        //    {
        //        Code = 167,
        //        Position = 6,
        //        SystemPageId = 4,
        //        PermissionId = 67,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Governats Key Value",
        //        NameEn = "Governats Key Value",
        //        ControllerName = "Governats",
        //        ActionName = "GovernatsKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Governats".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GovernatsKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(GovernatsKeyValue);
        //    #endregion

        //    #region Age Segments Permissions
        //    var searchAgeSegments = new Permission
        //    {
        //        Code = 115,
        //        Position = 1,
        //        SystemPageId = 10,
        //        PermissionId = 15,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Age Segments",
        //        NameEn = "View Age Segments",
        //        ControllerName = "AgeSegments",
        //        ActionName = "SearchAgeSegments",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "AgeSegments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchAgeSegments".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchAgeSegments);

        //    var createAgeSegment = new Permission
        //    {
        //        Code = 116,
        //        Position = 2,
        //        SystemPageId = 10,
        //        PermissionId = 16,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Age Segments",
        //        NameEn = "Create Age Segments",
        //        ControllerName = "AgeSegments",
        //        ActionName = "CreateAgeSegment",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "AgeSegments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateAgeSegment".Trim().ToLower()) == null)
        //        context.Permissions.Add(createAgeSegment);

        //    var updateAgeSegment = new Permission
        //    {
        //        Code = 117,
        //        Position = 3,
        //        SystemPageId = 10,
        //        PermissionId = 17,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Age Segments",
        //        NameEn = "Update Age Segments",
        //        ControllerName = "AgeSegments",
        //        ActionName = "UpdateAgeSegment",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "AgeSegments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateAgeSegment".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateAgeSegment);

        //    var deleteAgeSegment = new Permission
        //    {
        //        Code = 118,
        //        Position = 4,
        //        SystemPageId = 10,
        //        PermissionId = 18,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Age Segment",
        //        NameEn = "Delete Age Segment",
        //        ControllerName = "AgeSegments",
        //        ActionName = "DeleteAgeSegment",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "AgeSegments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteAgeSegment".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteAgeSegment);

        //    var getAgeSegmentForEdit = new Permission
        //    {
        //        Code = 119,
        //        Position = 5,
        //        SystemPageId = 10,
        //        PermissionId = 19,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Age Segment For Edit",
        //        NameEn = "Get Age Segment For Edit",
        //        ControllerName = "AgeSegments",
        //        ActionName = "GetAgeSegmentForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "AgeSegments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAgeSegmentForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAgeSegmentForEdit);
        //    #endregion

        //    #region Geo Zone Permissions
        //    var searchGeoZones = new Permission
        //    {
        //        Code = 161,
        //        Position = 1,
        //        SystemPageId = 5,
        //        PermissionId = 61,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Areas",
        //        NameEn = "View Areas",
        //        ControllerName = "GeoZones",
        //        ActionName = "SearchGeoZones",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchGeoZones".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchGeoZones);

        //    var addGeoZone = new Permission
        //    {
        //        Code = 163,
        //        Position = 2,
        //        SystemPageId = 5,
        //        PermissionId = 63,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Area",
        //        NameEn = "Create Area",
        //        ControllerName = "GeoZones",
        //        ActionName = "AddGeoZone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddGeoZone".Trim().ToLower()) == null)
        //        context.Permissions.Add(addGeoZone);

        //    var updateGeoZone = new Permission
        //    {
        //        Code = 164,
        //        Position = 3,
        //        SystemPageId = 5,
        //        PermissionId = 64,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Area",
        //        NameEn = "Update Area",
        //        ControllerName = "GeoZones",
        //        ActionName = "UpdateGeoZone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateGeoZone".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateGeoZone);

        //    var deleteGeoZone = new Permission
        //    {
        //        Code = 162,
        //        Position = 4,
        //        SystemPageId = 5,
        //        PermissionId = 62,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Area",
        //        NameEn = "Delete Area",
        //        ControllerName = "GeoZones",
        //        ActionName = "DeleteGeoZone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteGeoZone".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteGeoZone);

        //    var upoadKmlFile = new Permission
        //    {
        //        Code = 121,
        //        Position = 5,
        //        SystemPageId = 5,
        //        PermissionId = 21,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Upload Kml File",
        //        NameEn = "Upload Kml File",
        //        ControllerName = "Attachments",
        //        ActionName = "UpoadKmlFile",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Attachments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpoadKmlFile".Trim().ToLower()) == null)
        //        context.Permissions.Add(upoadKmlFile);

        //    var downloadKML = new Permission
        //    {
        //        Code = 123,
        //        Position = 6,
        //        SystemPageId = 5,
        //        PermissionId = 23,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Download KML File",
        //        NameEn = "Download KML File",
        //        ControllerName = "Attachments",
        //        ActionName = "DownloadKML",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Attachments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DownloadKML".Trim().ToLower()) == null)
        //        context.Permissions.Add(downloadKML);

        //    var getTimeZonesForGeoZone = new Permission
        //    {
        //        Code = 166,
        //        Position = 7,
        //        SystemPageId = 5,
        //        PermissionId = 66,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Time Zones For GeoZone",
        //        NameEn = "Get Time Zones For GeoZone",
        //        ControllerName = "GeoZones",
        //        ActionName = "GetTimeZonesForGeoZone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetTimeZonesForGeoZone".Trim().ToLower()) == null)
        //        context.Permissions.Add(getTimeZonesForGeoZone);

        //    var getGeoZoneForEdit = new Permission
        //    {
        //        Code = 165,
        //        Position = 8,
        //        SystemPageId = 5,
        //        PermissionId = 65,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Geo Zone For Edit",
        //        NameEn = "Get Geo Zone For Edit",
        //        ControllerName = "GeoZones",
        //        ActionName = "GetGeoZoneForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetGeoZoneForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getGeoZoneForEdit);

        //    var countriesKeyValue = new Permission
        //    {
        //        Code = 154,
        //        Position = 9,
        //        SystemPageId = 5,
        //        PermissionId = 54,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Countries Key Value",
        //        NameEn = "Countries Key Value",
        //        ControllerName = "Countries",
        //        ActionName = "CountriesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Countries".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CountriesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(countriesKeyValue);

        //    var geoZonesKeyValue = new Permission
        //    {
        //        Code = 160,
        //        Position = 10,
        //        SystemPageId = 5,
        //        PermissionId = 60,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Geo Zones Key Value",
        //        NameEn = "Geo Zones Key Value",
        //        ControllerName = "GeoZones",
        //        ActionName = "GeoZonesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "GeoZones".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GeoZonesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(geoZonesKeyValue);
        //    #endregion

        //    #region Request Second Visit Reasons Permissions
        //    var searchReasons = new Permission
        //    {
        //        Code = 193,
        //        Position = 1,
        //        SystemPageId = 6,
        //        PermissionId = 93,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Request Second Visit Reasons",
        //        NameEn = "View Request Second Visit Reasons",
        //        ControllerName = "Reasons",
        //        ActionName = "SearchReasons",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchReasons".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchReasons);

        //    var createReason = new Permission
        //    {
        //        Code = 194,
        //        Position = 2,
        //        SystemPageId = 6,
        //        PermissionId = 94,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Request Second Visit Reasons",
        //        NameEn = "Create Request Second Visit Reasons",
        //        ControllerName = "Reasons",
        //        ActionName = "CreateReason",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateReason".Trim().ToLower()) == null)
        //        context.Permissions.Add(createReason);

        //    var updateReason = new Permission
        //    {
        //        Code = 196,
        //        Position = 3,
        //        SystemPageId = 6,
        //        PermissionId = 96,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Request Second Visit Reasons",
        //        NameEn = "Update Request Second Visit Reasons",
        //        ControllerName = "Reasons",
        //        ActionName = "UpdateReason",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateReason".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateReason);

        //    var DeleteReason = new Permission
        //    {
        //        Code = 197,
        //        Position = 4,
        //        SystemPageId = 6,
        //        PermissionId = 97,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Request Second Visit Reasons",
        //        NameEn = "Delete Request Second Visit Reasons",
        //        ControllerName = "Reasons",
        //        ActionName = "DeleteReason",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteReason".Trim().ToLower()) == null)
        //        context.Permissions.Add(DeleteReason);

        //    var GetReasonForEdit = new Permission
        //    {
        //        Code = 195,
        //        Position = 5,
        //        SystemPageId = 6,
        //        PermissionId = 95,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Reason For Edit",
        //        NameEn = "Get Reason For Edit",
        //        ControllerName = "Reasons",
        //        ActionName = "GetReasonForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetReasonForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetReasonForEdit);

        //    var GetReasonActionsKeyValue = new Permission
        //    {
        //        Code = 198,
        //        Position = 6,
        //        SystemPageId = 6,
        //        PermissionId = 98,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Reason Actions Key Value",
        //        NameEn = "Get Reason Actions Key Value",
        //        ControllerName = "Reasons",
        //        ActionName = "GetReasonActionsKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reasons".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetReasonActionsKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetReasonActionsKeyValue);
        //    #endregion

        //    #region Re Assign Reasons Permissions

        //    #endregion

        //    #region Cancellation Reasons Permissions

        //    #endregion

        //    #region Reject Reasons Permissions

        //    #endregion

        //    #region Roles Permissions    
        //    var searchRoles = new Permission
        //    {
        //        Code = 205,
        //        Position = 1,
        //        SystemPageId = 11,
        //        PermissionId = 105,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Roles",
        //        NameEn = "View Roles",
        //        ControllerName = "Roles",
        //        ActionName = "SearchRoles",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchRoles".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchRoles);

        //    var createRole = new Permission
        //    {
        //        Code = 203,
        //        Position = 2,
        //        SystemPageId = 11,
        //        PermissionId = 103,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Role",
        //        NameEn = "Create Role",
        //        ControllerName = "Roles",
        //        ActionName = "CreateRole",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateRole".Trim().ToLower()) == null)
        //        context.Permissions.Add(createRole);

        //    var updateRole = new Permission
        //    {
        //        Code = 206,
        //        Position = 3,
        //        SystemPageId = 11,
        //        PermissionId = 106,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Role",
        //        NameEn = "Update Role",
        //        ControllerName = "Roles",
        //        ActionName = "UpdateRole",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateRole".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateRole);

        //    var deleteRole = new Permission
        //    {
        //        Code = 207,
        //        Position = 4,
        //        SystemPageId = 11,
        //        PermissionId = 107,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Role",
        //        NameEn = "Delete Role",
        //        ControllerName = "Roles",
        //        ActionName = "DeleteRole",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteRole".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteRole);

        //    var getRoleForEdit = new Permission
        //    {
        //        Code = 204,
        //        Position = 5,
        //        SystemPageId = 11,
        //        PermissionId = 104,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Role For Edit",
        //        NameEn = "Get Role For Edit",
        //        ControllerName = "Roles",
        //        ActionName = "GetRoleForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetRoleForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getRoleForEdit);

        //    var rolesKeyValue = new Permission
        //    {
        //        Code = 208,
        //        Position = 6,
        //        SystemPageId = 11,
        //        PermissionId = 108,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Roles Key Value",
        //        NameEn = "Roles Key Value",
        //        ControllerName = "Roles",
        //        ActionName = "RolesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Roles".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "RolesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(rolesKeyValue);
        //    #endregion

        //    #region User Permissions
        //    var searchClientUsers = new Permission
        //    {
        //        Code = 149,
        //        Position = 1,
        //        SystemPageId = 12,
        //        PermissionId = 49,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Users",
        //        NameEn = "View Users",
        //        ControllerName = "ClientUsers",
        //        ActionName = "SearchClientUsers",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchClientUsers".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchClientUsers);

        //    var createClientUser = new Permission
        //    {
        //        Code = 147,
        //        Position = 2,
        //        SystemPageId = 12,
        //        PermissionId = 47,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create User",
        //        NameEn = "Create User",
        //        ControllerName = "ClientUsers",
        //        ActionName = "CreateClientUser",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateClientUser".Trim().ToLower()) == null)
        //        context.Permissions.Add(createClientUser);

        //    var updateClientUser = new Permission
        //    {
        //        Code = 150,
        //        Position = 3,
        //        SystemPageId = 12,
        //        PermissionId = 50,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update User",
        //        NameEn = "Update User",
        //        ControllerName = "ClientUsers",
        //        ActionName = "UpdateClientUser",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateClientUser".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateClientUser);

        //    var deleteClientUser = new Permission
        //    {
        //        Code = 153,
        //        Position = 4,
        //        SystemPageId = 12,
        //        PermissionId = 53,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete User",
        //        NameEn = "Delete User",
        //        ControllerName = "ClientUsers",
        //        ActionName = "DeleteClientUser",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteClientUser".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteClientUser);

        //    var getUserPermission = new Permission
        //    {
        //        Code = 152,
        //        Position = 5,
        //        SystemPageId = 12,
        //        PermissionId = 52,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View User Permission",
        //        NameEn = "View User Permission",
        //        ControllerName = "ClientUsers",
        //        ActionName = "GetUserPermission",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetUserPermission".Trim().ToLower()) == null)
        //        context.Permissions.Add(getUserPermission);

        //    var updateClientUserPermission = new Permission
        //    {
        //        Code = 151,
        //        Position = 6,
        //        SystemPageId = 12,
        //        PermissionId = 51,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update User Permission",
        //        NameEn = "Update System Parameters",
        //        ControllerName = "ClientUsers",
        //        ActionName = "UpdateClientUserPermission",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateClientUserPermission".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateClientUserPermission);

        //    var GetClientUserForEdit = new Permission
        //    {
        //        Code = 148,
        //        Position = 7,
        //        SystemPageId = 12,
        //        PermissionId = 48,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "View System Parameters",
        //        NameEn = "View System Parameters",
        //        ControllerName = "ClientUsers",
        //        ActionName = "GetClientUserForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "ClientUsers".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetClientUserForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetClientUserForEdit);

        //    var UpoadUserImage = new Permission
        //    {
        //        Code = 120,
        //        Position = 7,
        //        SystemPageId = 12,
        //        PermissionId = 20,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Upoad User Image",
        //        NameEn = "Upoad User Image",
        //        ControllerName = "Attachments",
        //        ActionName = "UpoadUserImage",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Attachments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpoadUserImage".Trim().ToLower()) == null)
        //        context.Permissions.Add(UpoadUserImage);
        //    #endregion

        //    #region Visit Permissions
        //    var searchVisits = new Permission
        //    {
        //        Code = 241,
        //        Position = 1,
        //        SystemPageId = 13,
        //        PermissionId = 141,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Visits",
        //        NameEn = "View Visits",
        //        ControllerName = "Visit",
        //        ActionName = "SearchVisits",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchVisits".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchVisits);

        //    var getVisitDetailsByVisitId = new Permission
        //    {
        //        Code = 231,
        //        Position = 2,
        //        SystemPageId = 13,
        //        PermissionId = 131,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Visit Detail",
        //        NameEn = "View Visit Detail",
        //        ControllerName = "Visit",
        //        ActionName = "GetVisitDetailsByVisitId",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetVisitDetailsByVisitId".Trim().ToLower()) == null)
        //        context.Permissions.Add(getVisitDetailsByVisitId);

        //    var addVisitByChemistApp = new Permission
        //    {
        //        Code = 234,
        //        Position = 3,
        //        SystemPageId = 13,
        //        PermissionId = 134,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Visit",
        //        NameEn = "Add Visit",
        //        ControllerName = "Visit",
        //        ActionName = "AddVisitByChemistApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddVisitByChemistApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addVisitByChemistApp);

        //    var addPatient = new Permission
        //    {
        //        Code = 187,
        //        Position = 4,
        //        SystemPageId = 13,
        //        PermissionId = 87,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Patient",
        //        NameEn = "Add Patient",
        //        ControllerName = "Patients",
        //        ActionName = "AddPatient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddPatient".Trim().ToLower()) == null)
        //        context.Permissions.Add(addPatient);

        //    var addSecondVisitByChemistApp = new Permission
        //    {
        //        Code = 237,
        //        Position = 5,
        //        SystemPageId = 13,
        //        PermissionId = 137,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Request Second Visit",
        //        NameEn = "Request Second Visit",
        //        ControllerName = "Visit",
        //        ActionName = "AddSecondVisitByChemistApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddSecondVisitByChemistApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addSecondVisitByChemistApp);

        //    var createLostVisitTime = new Permission
        //    {
        //        Code = 232,
        //        Position = 6,
        //        SystemPageId = 13,
        //        PermissionId = 132,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Create Lost Visit Time",
        //        NameEn = "Create Lost Visit Time",
        //        ControllerName = "Visit",
        //        ActionName = "CreateLostVisitTime",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateLostVisitTime".Trim().ToLower()) == null)
        //        context.Permissions.Add(createLostVisitTime);

        //    var getAllLostVisitTimes = new Permission
        //    {
        //        Code = 233,
        //        Position = 7,
        //        SystemPageId = 13,
        //        PermissionId = 133,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get All Lost Visit Times",
        //        NameEn = "Get All Lost Visit Times",
        //        ControllerName = "Visit",
        //        ActionName = "GetAllLostVisitTimes",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAllLostVisitTimes".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAllLostVisitTimes);

        //    var holdVisit = new Permission
        //    {
        //        Code = 238,
        //        Position = 8,
        //        SystemPageId = 13,
        //        PermissionId = 138,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Hold Visit",
        //        NameEn = "Hold Visit",
        //        ControllerName = "Visit",
        //        ActionName = "HoldVisit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "HoldVisit".Trim().ToLower()) == null)
        //        context.Permissions.Add(holdVisit);

        //    var getAllAgeSegments = new Permission
        //    {
        //        Code = 239,
        //        Position = 9,
        //        SystemPageId = 13,
        //        PermissionId = 139,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get All AgeSegments",
        //        NameEn = "Get All AgeSegments",
        //        ControllerName = "Visit",
        //        ActionName = "GetAllAgeSegments",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAllAgeSegments".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAllAgeSegments);

        //    var getChemistRoutesFromVisit = new Permission
        //    {
        //        Code = 242,
        //        Position = 10,
        //        SystemPageId = 13,
        //        PermissionId = 142,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist Routes",
        //        NameEn = "Get Chemist Routes",
        //        ControllerName = "Visit",
        //        ActionName = "GetChemistRoutes",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistRoutes".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistRoutesFromVisit);

        //    var getSecondVisitTimeZoneAndDate = new Permission
        //    {
        //        Code = 236,
        //        Position = 11,
        //        SystemPageId = 13,
        //        PermissionId = 136,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Second Visit Time Zone And Date",
        //        NameEn = "Get Second Visit Time Zone And Date",
        //        ControllerName = "Visit",
        //        ActionName = "GetSecondVisitTimeZoneAndDate",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetSecondVisitTimeZoneAndDate".Trim().ToLower()) == null)
        //        context.Permissions.Add(getSecondVisitTimeZoneAndDate);

        //    var addPatientAddress = new Permission
        //    {
        //        Code = 183,
        //        Position = 12,
        //        SystemPageId = 13,
        //        PermissionId = 83,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Add Patient Address",
        //        NameEn = "Add Patient Address",
        //        ControllerName = "Patients",
        //        ActionName = "AddPatientAddress",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddPatientAddress".Trim().ToLower()) == null)
        //        context.Permissions.Add(addPatientAddress);

        //    var searchPatients = new Permission
        //    {
        //        Code = 225,
        //        Position = 13,
        //        SystemPageId = 13,
        //        PermissionId = 125,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Visit Search Patients",
        //        NameEn = "Visit Search Patients",
        //        ControllerName = "Visit",
        //        ActionName = "SearchPatients",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchPatients".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchPatients);

        //    var getPatientVisitsByPatientId = new Permission
        //    {
        //        Code = 226,
        //        Position = 14,
        //        SystemPageId = 13,
        //        PermissionId = 126,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Patient Visits By PatientId",
        //        NameEn = "Get Patient Visits By PatientId",
        //        ControllerName = "Visit",
        //        ActionName = "GetPatientVisitsByPatientId",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetPatientVisitsByPatientId".Trim().ToLower()) == null)
        //        context.Permissions.Add(getPatientVisitsByPatientId);

        //    var getAvailableVisitsInArea = new Permission
        //    {
        //        Code = 227,
        //        Position = 15,
        //        SystemPageId = 13,
        //        PermissionId = 127,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Available Visits In Area",
        //        NameEn = "Get Available Visits In Area",
        //        ControllerName = "Visit",
        //        ActionName = "GetAvailableVisitsInArea",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAvailableVisitsInArea".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAvailableVisitsInArea);

        //    var getAvailableVisitsInAreaWeb = new Permission
        //    {
        //        Code = 229,
        //        Position = 16,
        //        SystemPageId = 13,
        //        PermissionId = 129,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Available Visits In Area Web",
        //        NameEn = "Get Available Visits In Area Web",
        //        ControllerName = "Visit",
        //        ActionName = "GetAvailableVisitsInAreaWeb",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAvailableVisitsInAreaWeb".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAvailableVisitsInAreaWeb);

        //    var getAvailableVisitsForChemist = new Permission
        //    {
        //        Code = 230,
        //        Position = 17,
        //        SystemPageId = 13,
        //        PermissionId = 130,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Available Visits For Chemist",
        //        NameEn = "Get Available Visits For Chemist",
        //        ControllerName = "Visit",
        //        ActionName = "GetAvailableVisitsForChemist",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAvailableVisitsForChemist".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAvailableVisitsForChemist);

        //    var upoadVisitData = new Permission
        //    {
        //        Code = 122,
        //        Position = 18,
        //        SystemPageId = 13,
        //        PermissionId = 22,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Upload Visit Data",
        //        NameEn = "Upload Visit Data",
        //        ControllerName = "Attachments",
        //        ActionName = "UpoadVisitData",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Attachments".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpoadVisitData".Trim().ToLower()) == null)
        //        context.Permissions.Add(upoadVisitData);
        //    #endregion

        //    #region Chemist Permissions
        //    var SearchChemists = new Permission
        //    {
        //        Code = 125,
        //        Position = 1,
        //        SystemPageId = 14,
        //        PermissionId = 25,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Chemist",
        //        NameEn = "View Chemist",
        //        ControllerName = "Chemist",
        //        ActionName = "SearchChemists",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchChemists".Trim().ToLower()) == null)
        //        context.Permissions.Add(SearchChemists);

        //    var createChemist = new Permission
        //    {
        //        Code = 124,
        //        Position = 2,
        //        SystemPageId = 14,
        //        PermissionId = 24,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Chemist",
        //        NameEn = "Create Chemist",
        //        ControllerName = "Chemist",
        //        ActionName = "CreateChemist",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateChemist".Trim().ToLower()) == null)
        //        context.Permissions.Add(createChemist);

        //    var updateChemist = new Permission
        //    {
        //        Code = 126,
        //        Position = 3,
        //        SystemPageId = 14,
        //        PermissionId = 26,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Chemist",
        //        NameEn = "Update Chemist",
        //        ControllerName = "Chemist",
        //        ActionName = "UpdateChemist",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateChemist".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateChemist);

        //    var deleteChemist = new Permission
        //    {
        //        Code = 127,
        //        Position = 4,
        //        SystemPageId = 14,
        //        PermissionId = 27,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Chemist",
        //        NameEn = "Delete Chemist",
        //        ControllerName = "Chemist",
        //        ActionName = "DeleteChemist",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteChemist".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteChemist);

        //    var searchChemistSchedule = new Permission
        //    {
        //        Code = 137,
        //        Position = 5,
        //        SystemPageId = 14,
        //        PermissionId = 37,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Chemist Schedule",
        //        NameEn = "View Chemist Schedule",
        //        ControllerName = "Chemist",
        //        ActionName = "SearchChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchChemistSchedule);

        //    var createChemistSchedule = new Permission
        //    {
        //        Code = 131,
        //        Position = 6,
        //        SystemPageId = 14,
        //        PermissionId = 31,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Chemist Schedule",
        //        NameEn = "Create Chemist Schedule",
        //        ControllerName = "Chemist",
        //        ActionName = "CreateChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(createChemistSchedule);

        //    var updateChemistSchedule = new Permission
        //    {
        //        Code = 132,
        //        Position = 7,
        //        SystemPageId = 14,
        //        PermissionId = 32,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Chemist Schedule",
        //        NameEn = "Update Chemist Schedule",
        //        ControllerName = "Chemist",
        //        ActionName = "UpdateChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateChemistSchedule);

        //    var deleteChemistSchedule = new Permission
        //    {
        //        Code = 133,
        //        Position = 8,
        //        SystemPageId = 14,
        //        PermissionId = 33,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Chemist Schedule",
        //        NameEn = "Delete Chemist Schedule",
        //        ControllerName = "Chemist",
        //        ActionName = "DeleteChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteChemistSchedule);

        //    var searchChemistPermits = new Permission
        //    {
        //        Code = 141,
        //        Position = 9,
        //        SystemPageId = 14,
        //        PermissionId = 41,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Chemist Permits",
        //        NameEn = "View Chemist Permits",
        //        ControllerName = "Chemist",
        //        ActionName = "SearchChemistPermits",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchChemistPermits".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchChemistPermits);

        //    var createChemistPermit = new Permission
        //    {
        //        Code = 138,
        //        Position = 10,
        //        SystemPageId = 14,
        //        PermissionId = 38,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Chemist Permit",
        //        NameEn = "Create Chemist Permit",
        //        ControllerName = "Chemist",
        //        ActionName = "CreateChemistPermit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "CreateChemistPermit".Trim().ToLower()) == null)
        //        context.Permissions.Add(createChemistPermit);

        //    var updateChemistPermit = new Permission
        //    {
        //        Code = 139,
        //        Position = 11,
        //        SystemPageId = 14,
        //        PermissionId = 39,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Chemist Permit",
        //        NameEn = "Update Chemist Permit",
        //        ControllerName = "Chemist",
        //        ActionName = "UpdateChemistPermit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateChemistPermit".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateChemistPermit);

        //    var deleteChemistPermit = new Permission
        //    {
        //        Code = 140,
        //        Position = 12,
        //        SystemPageId = 14,
        //        PermissionId = 40,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Chemist Permit",
        //        NameEn = "Delete Chemist Permit",
        //        ControllerName = "Chemist",
        //        ActionName = "DeleteChemistPermit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteChemistPermit".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteChemistPermit);

        //    var getChemistRoutesFromChemist = new Permission
        //    {
        //        Code = 143,
        //        Position = 13,
        //        SystemPageId = 14,
        //        PermissionId = 43,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist Routes",
        //        NameEn = "Get Chemist Routes",
        //        ControllerName = "Chemist",
        //        ActionName = "GetChemistRoutes",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistRoutes".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistRoutesFromChemist);

        //    var geoChemistZonesKeyValue = new Permission
        //    {
        //        Code = 128,
        //        Position = 14,
        //        SystemPageId = 14,
        //        PermissionId = 28,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Geo Chemist Zones Key Value",
        //        NameEn = "Geo Chemist Zones Key Value",
        //        ControllerName = "Chemist",
        //        ActionName = "GeoChemistZonesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GeoChemistZonesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(geoChemistZonesKeyValue);

        //    var getChemistScheduleForEdit = new Permission
        //    {
        //        Code = 135,
        //        Position = 14,
        //        SystemPageId = 14,
        //        PermissionId = 35,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist Schedule For Edit",
        //        NameEn = "Get Chemist Schedule For Edit",
        //        ControllerName = "Chemist",
        //        ActionName = "GetChemistScheduleForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistScheduleForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistScheduleForEdit);

        //    var chemistsKeyValue = new Permission
        //    {
        //        Code = 129,
        //        Position = 16,
        //        SystemPageId = 14,
        //        PermissionId = 29,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Chemists Key Value",
        //        NameEn = "Chemists Key Value",
        //        ControllerName = "Chemist",
        //        ActionName = "ChemistsKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "ChemistsKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(chemistsKeyValue);

        //    var getChemistForEdit = new Permission
        //    {
        //        Code = 130,
        //        Position = 17,
        //        SystemPageId = 14,
        //        PermissionId = 30,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist For Edit",
        //        NameEn = "Get Chemist For Edit",
        //        ControllerName = "Chemist",
        //        ActionName = "GetChemistForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistForEdit);

        //    var getChemistPermitForEdit = new Permission
        //    {
        //        Code = 142,
        //        Position = 18,
        //        SystemPageId = 14,
        //        PermissionId = 42,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist Permit For Edit",
        //        NameEn = "Get Chemist Permit For Edit",
        //        ControllerName = "Chemist",
        //        ActionName = "GetChemistPermitForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistPermitForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistPermitForEdit);

        //    var duplicateChemistSchedule = new Permission
        //    {
        //        Code = 134,
        //        Position = 19,
        //        SystemPageId = 14,
        //        PermissionId = 34,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Duplicate Chemist Schedule",
        //        NameEn = "Duplicate Chemist Schedule",
        //        ControllerName = "Chemist",
        //        ActionName = "DuplicateChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DuplicateChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(duplicateChemistSchedule);

        //    var getChemistAssignedGeoZonesKeyValue = new Permission
        //    {
        //        Code = 136,
        //        Position = 20,
        //        SystemPageId = 14,
        //        PermissionId = 36,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Chemist Assigned Geo Zones Key Value",
        //        NameEn = "Get Chemist Assigned Geo Zones Key Value",
        //        ControllerName = "Chemist",
        //        ActionName = "GetChemistAssignedGeoZonesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Chemist".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetChemistAssignedGeoZonesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(getChemistAssignedGeoZonesKeyValue);
        //    #endregion

        //    #region Reports Permissions
        //    var getVisitReportDetailed = new Permission
        //    {
        //        Code = 199,
        //        Position = 1,
        //        SystemPageId = 15,
        //        PermissionId = 99,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Visit Report Detailed",
        //        NameEn = "View Visit Report Detailed",
        //        ControllerName = "Reports",
        //        ActionName = "GetDetailedVisitReport",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reports".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetDetailedVisitReport".Trim().ToLower()) == null)
        //        context.Permissions.Add(getVisitReportDetailed);

        //    var getVisitReportTotal = new Permission
        //    {
        //        Code = 200,
        //        Position = 2,
        //        SystemPageId = 15,
        //        PermissionId = 100,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Visit Report Total",
        //        NameEn = "View Visit Report Total",
        //        ControllerName = "Reports",
        //        ActionName = "GetTotalVisitReport",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reports".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetTotalVisitReport".Trim().ToLower()) == null)
        //        context.Permissions.Add(getVisitReportTotal);

        //    var getCanceledVisitReport = new Permission
        //    {
        //        Code = 201,
        //        Position = 3,
        //        SystemPageId = 15,
        //        PermissionId = 101,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Canceled Visit Report",
        //        NameEn = "View Canceled Visit Report",
        //        ControllerName = "Reports",
        //        ActionName = "GetCanceledVisitReport",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reports".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetCanceledVisitReport".Trim().ToLower()) == null)
        //        context.Permissions.Add(getCanceledVisitReport);

        //    var GetRejectedVisitReport = new Permission
        //    {
        //        Code = 202,
        //        Position = 4,
        //        SystemPageId = 15,
        //        PermissionId = 102,
        //        IsActive = true,
        //        IsForDisplay = true,
        //        IsNeedToAuthorized = true,
        //        NameAr = "View Rejected Visit Report",
        //        NameEn = "View Rejected Visit Report",
        //        ControllerName = "Reports",
        //        ActionName = "GetRejectedVisitReport",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Reports".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetRejectedVisitReport".Trim().ToLower()) == null)
        //        context.Permissions.Add(GetRejectedVisitReport);
        //    #endregion

        //    #region Super Admin Add Clients
        //    var addClient = new Permission
        //    {
        //        Code = 144,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 44,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Create Client",
        //        NameEn = "Create Client",
        //        ControllerName = "Client",
        //        ActionName = "AddClient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Client".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddClient".Trim().ToLower()) == null)
        //        context.Permissions.Add(addClient);

        //    var updateClient = new Permission
        //    {
        //        Code = 145,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 45,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Update Client",
        //        NameEn = "Update Client",
        //        ControllerName = "Client",
        //        ActionName = "UpdateClient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Client".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdateClient".Trim().ToLower()) == null)
        //        context.Permissions.Add(updateClient);

        //    var deleteClient = new Permission
        //    {
        //        Code = 146,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 46,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Delete Client",
        //        NameEn = "Delete Client",
        //        ControllerName = "Client",
        //        ActionName = "DeleteClient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Client".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeleteClient".Trim().ToLower()) == null)
        //        context.Permissions.Add(deleteClient);
        //    #endregion

        //    #region City Permissions
        //    var postCity = new Permission
        //    {
        //        Code = 243,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 143,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "View System Parameters",
        //        NameEn = "View System Parameters",
        //        ControllerName = "City",
        //        ActionName = "Post",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "City".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "Post".Trim().ToLower()) == null)
        //        context.Permissions.Add(postCity);

        //    var getCity = new Permission
        //    {
        //        Code = 244,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 144,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "View System Parameters",
        //        NameEn = "View System Parameters",
        //        ControllerName = "City",
        //        ActionName = "Get",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "City".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "Get".Trim().ToLower()) == null)
        //        context.Permissions.Add(getCity);
        //    #endregion

        //    #region Patiant Pages Permissions
        //    var addOrUpdatePatientByPatientApp = new Permission
        //    {
        //        Code = 181,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 81,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Or Update Patient By Patient App",
        //        NameEn = "Add Or Update Patient By Patient App",
        //        ControllerName = "Patients",
        //        ActionName = "AddOrUpdatePatientByPatientApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddOrUpdatePatientByPatientApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addOrUpdatePatientByPatientApp);

        //    var addPatientAddressByPatientApp = new Permission
        //    {
        //        Code = 182,
        //        Position = 2,
        //        SystemPageId = 1,
        //        PermissionId = 82,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Patient Address By Patient App",
        //        NameEn = "Add Patient Address By Patient App",
        //        ControllerName = "Patients",
        //        ActionName = "AddPatientAddressByPatientApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddPatientAddressByPatientApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addPatientAddressByPatientApp);

        //    var searchPatientSchedule = new Permission
        //    {
        //        Code = 240,
        //        Position = 3,
        //        SystemPageId = 1,
        //        PermissionId = 140,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Search Patient Schedule",
        //        NameEn = "Search Patient Schedule",
        //        ControllerName = "Visit",
        //        ActionName = "SearchPatientSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SearchPatientSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(searchPatientSchedule);

        //    var sendPatientAction = new Permission
        //    {
        //        Code = 228,
        //        Position = 4,
        //        SystemPageId = 1,
        //        PermissionId = 128,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Send Patient Action",
        //        NameEn = "Send Patient Action",
        //        ControllerName = "Visit",
        //        ActionName = "SendPatientAction",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SendPatientAction".Trim().ToLower()) == null)
        //        context.Permissions.Add(sendPatientAction);

        //    var getPatientForEdit = new Permission
        //    {
        //        Code = 188,
        //        Position = 5,
        //        SystemPageId = 1,
        //        PermissionId = 88,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Patient For Edit",
        //        NameEn = "Get Patient For Edit",
        //        ControllerName = "Patients",
        //        ActionName = "GetPatientForEdit",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetPatientForEdit".Trim().ToLower()) == null)
        //        context.Permissions.Add(getPatientForEdit);

        //    var addVisitByPatientApp = new Permission
        //    {
        //        Code = 235,
        //        Position = 6,
        //        SystemPageId = 13,
        //        PermissionId = 135,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Visit By Patient App",
        //        NameEn = "Add Visit By Patient App",
        //        ControllerName = "Visit",
        //        ActionName = "AddVisitByPatientApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Visit".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddVisitByPatientApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addVisitByPatientApp);

        //    var addPatientPhoneByPatientApp = new Permission
        //    {
        //        Code = 184,
        //        Position = 7,
        //        SystemPageId = 1,
        //        PermissionId = 84,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Add Patient Phone By Patient App",
        //        NameEn = "Add Patient Phone By Patient App",
        //        ControllerName = "Patients",
        //        ActionName = "AddPatientPhoneByPatientApp",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddPatientPhoneByPatientApp".Trim().ToLower()) == null)
        //        context.Permissions.Add(addPatientPhoneByPatientApp);

        //    var addPatientPhone = new Permission
        //    {
        //        Code = 185,
        //        Position = 8,
        //        SystemPageId = 1,
        //        PermissionId = 85,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Add Patient Phone",
        //        NameEn = "Add Patient Phone",
        //        ControllerName = "Patients",
        //        ActionName = "AddPatientPhone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddPatientPhone".Trim().ToLower()) == null)
        //        context.Permissions.Add(addPatientPhone);

        //    var patientsList = new Permission
        //    {
        //        Code = 186,
        //        Position = 9,
        //        SystemPageId = 12,
        //        PermissionId = 86,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Patients List",
        //        NameEn = "Patients List",
        //        ControllerName = "Patients",
        //        ActionName = "PatientsList",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "PatientsList".Trim().ToLower()) == null)
        //        context.Permissions.Add(patientsList);

        //    var updatePatient = new Permission
        //    {
        //        Code = 189,
        //        Position = 10,
        //        SystemPageId = 1,
        //        PermissionId = 89,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Update Patient",
        //        NameEn = "Update Patient",
        //        ControllerName = "Patients",
        //        ActionName = "UpdatePatient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "UpdatePatient".Trim().ToLower()) == null)
        //        context.Permissions.Add(updatePatient);

        //    var deletePatientAddress = new Permission
        //    {
        //        Code = 190,
        //        Position = 11,
        //        SystemPageId = 1,
        //        PermissionId = 90,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Delete Patient Address",
        //        NameEn = "Delete Patient Address",
        //        ControllerName = "Patients",
        //        ActionName = "DeletePatientAddress",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeletePatientAddress".Trim().ToLower()) == null)
        //        context.Permissions.Add(deletePatientAddress);

        //    var deletePatientPhone = new Permission
        //    {
        //        Code = 191,
        //        Position = 12,
        //        SystemPageId = 1,
        //        PermissionId = 91,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Delete Patient Phone",
        //        NameEn = "Delete Patient Phone",
        //        ControllerName = "Patients",
        //        ActionName = "DeletePatientPhone",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeletePatientPhone".Trim().ToLower()) == null)
        //        context.Permissions.Add(deletePatientPhone);

        //    var deletePatient = new Permission
        //    {
        //        Code = 192,
        //        Position = 13,
        //        SystemPageId = 1,
        //        PermissionId = 92,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Delete Patient",
        //        NameEn = "Delete Patient",
        //        ControllerName = "Patients",
        //        ActionName = "DeletePatient",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Patients".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "DeletePatient".Trim().ToLower()) == null)
        //        context.Permissions.Add(deletePatient);
        //    #endregion

        //    #region Scedule
        //    //For Chemist User
        //    var MySchedule = new Permission
        //    {
        //        Code = 209,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 109,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "My Schedule",
        //        NameEn = "My Schedule",
        //        ControllerName = "Schedule",
        //        ActionName = "MySchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "MySchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(MySchedule);

        //    //For Chemist User
        //    var sendSchedule = new Permission
        //    {
        //        Code = 210,
        //        Position = 2,
        //        SystemPageId = 1,
        //        PermissionId = 110,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Send",
        //        NameEn = "Send",
        //        ControllerName = "Schedule",
        //        ActionName = "Send",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "Send".Trim().ToLower()) == null)
        //        context.Permissions.Add(sendSchedule);

        //    //For Chemist User
        //    var sendChemistTracking = new Permission
        //    {
        //        Code = 211,
        //        Position = 3,
        //        SystemPageId = 1,
        //        PermissionId = 111,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Send Chemist Tracking",
        //        NameEn = "Send Chemist Tracking",
        //        ControllerName = "Schedule",
        //        ActionName = "SendChemistTracking",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SendChemistTracking".Trim().ToLower()) == null)
        //        context.Permissions.Add(sendChemistTracking);

        //    //For Chemist User
        //    var getAllVisitNotifications = new Permission
        //    {
        //        Code = 213,
        //        Position = 4,
        //        SystemPageId = 1,
        //        PermissionId = 113,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = true,
        //        NameAr = "Get All Visit Notifications",
        //        NameEn = "Get All Visit Notifications",
        //        ControllerName = "Schedule",
        //        ActionName = "GetAllVisitNotifications",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetAllVisitNotifications".Trim().ToLower()) == null)
        //        context.Permissions.Add(getAllVisitNotifications);

        //    //For Chemist User
        //    var chemistSchedule = new Permission
        //    {
        //        Code = 216,
        //        Position = 5,
        //        SystemPageId = 1,
        //        PermissionId = 116,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Chemist Schedule",
        //        NameEn = "Chemist Schedule",
        //        ControllerName = "Schedule",
        //        ActionName = "ChemistSchedule",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "ChemistSchedule".Trim().ToLower()) == null)
        //        context.Permissions.Add(chemistSchedule);

        //    var getActionReasons = new Permission
        //    {
        //        Code = 214,
        //        Position = 6,
        //        SystemPageId = 1,
        //        PermissionId = 114,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get Action Reasons",
        //        NameEn = "Get Action Reasons",
        //        ControllerName = "Schedule",
        //        ActionName = "GetActionReasons",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetActionReasons".Trim().ToLower()) == null)
        //        context.Permissions.Add(getActionReasons);

        //    var addUserDevice = new Permission
        //    {
        //        Code = 215,
        //        Position = 7,
        //        SystemPageId = 1,
        //        PermissionId = 115,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Add User Device",
        //        NameEn = "Add User Device",
        //        ControllerName = "Schedule",
        //        ActionName = "AddUserDevice",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "AddUserDevice".Trim().ToLower()) == null)
        //        context.Permissions.Add(addUserDevice);

        //    var chemistGPSTracking = new Permission
        //    {
        //        Code = 217,
        //        Position = 8,
        //        SystemPageId = 1,
        //        PermissionId = 117,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Chemist GPS Tracking",
        //        NameEn = "Chemist GPS Tracking",
        //        ControllerName = "Schedule",
        //        ActionName = "ChemistGPSTracking",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "ChemistGPSTracking".Trim().ToLower()) == null)
        //        context.Permissions.Add(chemistGPSTracking);

        //    var sendChemistAction = new Permission
        //    {
        //        Code = 212,
        //        Position = 9,
        //        SystemPageId = 1,
        //        PermissionId = 112,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Send Chemist Action",
        //        NameEn = "Send Chemist Action",
        //        ControllerName = "Schedule",
        //        ActionName = "SendChemistAction",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "Schedule".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "SendChemistAction".Trim().ToLower()) == null)
        //        context.Permissions.Add(sendChemistAction);
        //    #endregion

        //    #region General Action Not Need Permissions
        //    var getSystemPagesTree = new Permission
        //    {
        //        Code = 218,
        //        Position = 1,
        //        SystemPageId = 1,
        //        PermissionId = 118,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get System Pages Tree",
        //        NameEn = "Get System Pages Tree",
        //        ControllerName = "SystemPages",
        //        ActionName = "GetSystemPagesTree",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemPages".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetSystemPagesTree".Trim().ToLower()) == null)
        //        context.Permissions.Add(getSystemPagesTree);

        //    var getSystemPagesKeyValue = new Permission
        //    {
        //        Code = 219,
        //        Position = 2,
        //        SystemPageId = 1,
        //        PermissionId = 119,
        //        IsActive = true,
        //        IsForDisplay = false,
        //        IsNeedToAuthorized = false,
        //        NameAr = "Get System Pages Key Value",
        //        NameEn = "Get System Pages Key Value",
        //        ControllerName = "SystemPages",
        //        ActionName = "GetSystemPagesKeyValue",
        //    };
        //    if (permissions.FirstOrDefault(p => p.ControllerName.Trim().ToLower() == "SystemPages".Trim().ToLower()
        //        && p.ActionName.Trim().ToLower() == "GetSystemPagesKeyValue".Trim().ToLower()) == null)
        //        context.Permissions.Add(getSystemPagesKeyValue);
        //    #endregion

        //    context.SaveChanges();
        //}
    }
}
