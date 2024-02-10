import { PagesEnum } from "../models/pages";

export class Assets {
    private static codeAssets: CodeAsset[] = [
        {
            code: PagesEnum.Dashboard,
            img: "assets/home.svg",
            url: "/client/dashboard"

        },
         {
            code: PagesEnum.SystemConfiguration,
            img: "assets/settings.svg",
            url: "/system-configuration"

        },
        {
            code: PagesEnum.UserManagement,
            img: "assets/clients.svg",
            url: null
        },
        {
            code: PagesEnum.Users,
            img: "assets/clients.svg",
            url: "/users/users-list"
        },
        {
            code: PagesEnum.Roles,
            img: "assets/clients.svg",
            url: "/client/role-list"
        },
        {
            code: PagesEnum.HomeVisit,
            img: "assets/home-visits.svg",
            url: null
        },
        {
            code:PagesEnum.AddNewVisit,
            img: "./../../../../assets/home-visitsAdd.png",
            url: "/visits/home-visits-create"
        },
        {
            code: PagesEnum.ViewVisit,
            img: "assets/home-visits.svg",
            url: "/visits/visits-list"
        },
        {
            code: PagesEnum.QueryTime,
            url: "/visits/query-avliable-time",
            img: "./../../../../assets/home-visitsView.png"
        },
        {
            code: PagesEnum.Chemists,
            url: null,
            img: "assets/chemist.svg"
        },
        {
            code: PagesEnum.AddNewChemists,
            url: '/chemists/chemists-create',
            img: "./../../../../assets/chemistAdd.png"
        },
        {
            code: PagesEnum.ViewChemists,
            url: '/chemists/chemists-list',
            img: "./../../../../assets/chemistView.png"
        },
        {
            code: PagesEnum.TrackChemists,
            url: '/chemists/track',
            img: "./../../../../assets/chemistView.png"
        },
        {
            code: PagesEnum.Reports,
            url: null,
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.VisitReports,
            url: "/Client/reports/visit-reports/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.TATTracking,
            url: "/Client/reports/tat-tracking/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.ReAssignedReport,
            url: "/Client/reports/reassigned-reports/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.LostBusinessReport,
            url: "/Client/reports/lost-bussiness-reports/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.CancelledVisitReport,
            url: "/Client/reports/cancelled-visit-reports/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.RejectedReport,
            url: "/Client/reports/rejected-reports/create",
            img: "assets/reports.svg"
        },
        {
            code: PagesEnum.Patient,
            url: "/users/patients-list",
            img: "assets/patient.svg"
        },
    ];
    static getImageByCode(code: number) {
        return Assets.codeAssets.find(x => x.code == code).img;
    }
    static getUrlByCode(code: number) {
        return Assets.codeAssets.find(x => x.code == code).url;
    }
}

export interface CodeAsset {
    code: number;
    img: string;
    url: string;
}