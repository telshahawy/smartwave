import { environment } from "src/environments/environment";

const BASE_URL= environment.baseUrl;

const API_URL=BASE_URL+"api/"
const REASONS = 'Reasons/';
const SEARCH_REASONS = 'SearchReasons';
const GET_FOR_EDIT_REASONS = 'GetReasonForEdit';
const CREATE_REASONS = 'CreateReason';
const UPDATE_REASONS = 'UpdateReason';
const DELETE_REASONS = 'DeleteReason';
const GET_REASONS_ACTIONS_KEY_VALUE_REASONS = 'GetReasonActionsKeyValue';
const AGE_SEGMENTS='AgeSegments/'
const CHEMIST='Chemist'
const SYSTEMPARAMETERS='SystemParameters'
const SETUP="Setup/";
const HOME='HomePage/'
const REPORTS='Reports/'


export abstract class EndPoints {
  public static SEARCH_REASONS_ENDPOINT = API_URL + REASONS + SEARCH_REASONS;
  public static GET_FOR_EDIT_REASONS_ENDPOINT = API_URL + REASONS + GET_FOR_EDIT_REASONS;
  public static CREATE_REASONS_ENDPOINT = API_URL + REASONS + CREATE_REASONS;
  public static UPDATE_REASONS_ENDPOINT = API_URL + REASONS + UPDATE_REASONS;
  public static DELETE_REASONS_ENDPOINT = API_URL + REASONS + DELETE_REASONS;
  public static GET_REASONS_ACTIONS_KEY_VALUE_REASONS_ENDPOINT = API_URL + REASONS + GET_REASONS_ACTIONS_KEY_VALUE_REASONS;
  public static AGE_SEGMENTS_ENDPOINT = API_URL  + AGE_SEGMENTS;
  public static CHEMIST_ENDPOINT = API_URL  + CHEMIST;
  public static SYSTEMPARAMETERS_ENDPOINT = API_URL  + SETUP +SYSTEMPARAMETERS;
  public static HOME_ENDPOINT = API_URL+HOME;
  public static REPORTS_ENDPOINT = API_URL+REPORTS;

}
