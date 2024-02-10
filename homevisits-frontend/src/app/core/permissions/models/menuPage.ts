import { MenuElements } from "./menuElement";

export interface MenuPage {
    systemPageId: number;
    code: number;
    name: string;
    position: number;
    parentId: number;
    hasUrl: boolean;
    menuElements: MenuElements;
    children: MenuPage[]
}
export interface  SystemPageTreeDto
    {
        Id : number;
        name: string;
        code: string;
        hasUrl : boolean;
        parentId: number;
        permissions: PermissionTreeDto[];
    }

    export interface   PermissionTreeDto
    {
        systemPagePermissionId : number;
        permissionId: number;
        number: string;
        permissionCode: number;
    }