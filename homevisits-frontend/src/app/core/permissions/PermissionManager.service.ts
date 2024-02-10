import { ActionsEnum } from "./models/actions";
import { PagesEnum } from "./models/pages";
import { UserPermission } from "./models/userPermisions";

export class PermissionService {
    constructor() {
        this.userPermissions = JSON.parse(sessionStorage.getItem('permissions'));
    }
    userPermissions: UserPermission[]

    isGranted(page: PagesEnum, action: ActionsEnum) {
        let permission = this.userPermissions != null && this.userPermissions.some(x => x.pageCode == page && x.actionCode == action);
        return permission;
    }
}