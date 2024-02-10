import { ActionsEnum } from "./actions";
import { PagesEnum } from "./pages";

export interface PageAction {
    page: PagesEnum,
    action : ActionsEnum
}