import { Time } from '@angular/common';
import { StorageStrategies } from 'ngx-webstorage';

export interface IpagedList<T> {
  response: T;
}

// chemists
export class ChemistsListDto {
  chemistId: string;
  code: string;
  name: string;
  gender: number;
  age: number;
  genderName: string;
  phoneNumber: string;
  govenateName: string;
  countryName: string;
  geoZoneName: string;
  joinDate: Date;
  isActive: boolean;
}
export class ChemistTrackingLog {
  chemistId: string;
  name: string;
  phoneNumber: string;
  latitude: number;
  longitude: number;
  lastTrackingTime: string;
  mobileBatteryPercentage: number;
  visitNo: string;
  visitDate: string;
  visitTime: object;
  locationUrl: string;
  timePassed: string;
  areaName: string;
}
export class ChemistsSearchCriteria {
  governateId: string;
  joinDateFrom: Date;
  joinDateto: Date;
  countryId: string;
  pageSize: number;
  currentPageIndex: number;
  gender: number;
  code: string;
  chemistName: string;
  phoneNo: string;
  areaAssignStatus: any;
  chemistStatus: boolean;
  expertChemist: boolean;
  geoZoneId: string;
}
export class ChemistPermitSearchCriteria {
  chemistId: string;
  permitDate: Date;
}
export class GetChemistsLastTracking {
  name: string;
  currentPageIndex: number;
  pageSize: number;
}
export class ChemistVisitsScheduleModel {
  startTime: object;
  endTime: string;
  areaName: string;
  patientAddress: string;
  patientName: string;
  statusName: string;
}
export class CreateChemistPermitModel {
  chemistId: string;
  permitDate: Date;
  startTime: object;
  endTime: object;
}
export class UpdateChemistPermitModel {
  permitDate: Date;
  startTime: object;
  endTime: object;
}
export class ChemistCreateDto {
  userName: string;
  name: string;
  password: string;
  gender: number;
  phoneNumber: string;
  birthDate: Date;
  personalPhoto: string;
  expertChemist: boolean;
  isActive: true;
  joinDate: Date;
  dob: number;
  geoZoneIds: string[];
}
export class ChemistUpdateDto {
  userId: string;
  name: string;
  gender: number;
  phoneNumber: string;
  birthDate: Date;
  personalPhoto: string;
  expertChemist: boolean;
  isActive: true;
  joinDate: Date;
  geoZoneIds: string[];
}
export class ChemistViewResponse {
  response: ChemistViewSecondResponse;
}
export class ChemistPermitViewResponse {
  response: ChemistPermitViewSecondResponse
}
export class ChemistPermitViewSecondResponse {
  permit: ChemistPermitViewDto
}
export class ChemistPermitViewDto {
  chemistPermitId: string;
  chemistId: string;
  permitDate: Date;
  startTime: object;
  endTime: object;
  createdAt: Date;
  createdBy: string;
}
export class ChemistViewSecondResponse {
  chemist: ChemistViewDto;
}
export class ChemistViewDto {
  chemistId: string;
  code: string;
  clientId: string;
  age: number;
  name: string;
  gender: number;
  phoneNumber: string;
  birthdate: Date;
  personalPhoto: string;
  expertChemist: boolean;
  isActive: true;
  joinDate: Date;
  geoZones: GeoZones[];
  userName: string;
}
export class ChemistListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  chemists: ChemistsListDto[];
}
export class ChemistPermitListResponse {
  permits: ChemistPermitDto[];
}
export class ChemistPermitDto {
  chemistPermitId: string;
  permitDate: Date;
  startTime: any;
  endTime: any;
}
export class ChemistVisitsScheduleListResponse {
  response: ChemistVisitsScheduleModel[];
}
export class ChemistLastTrackingLogListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  chemistLastTrackingLogs: ChemistTrackingLog[];
}
export class CountryLookup {
  countryId: number;
  name: number;
}
export class CountryList {
  countries: CountryLookup[];
}

export class AreaLookup {
  geoZoneId: number;
  name: number;
}
export class AreaList {
  geoZones: AreaLookup[];
}
export class GovernateLookup {
  governateId: number;
  name: number;
}
export class GovernateList {
  governats: GovernateLookup[];
}
export enum Gender {
  'Male' = 1,
  'Female' = 2,
}

export enum ExpertChemist {
  'Yes' = 'true',
  'No' = 'false',
}
export enum chemistStatus {
  'Active' = 'true',
  'Inactive' = 'false',
}
export enum areaStatus {
  'Assigned' = 'true',
  'Not Assigned' = 'false',
}
export enum areaActiveStatus {
  'Active' = 'true',
  'Inactive' = 'false',
}
export class UpdateReponse {
  response: boolean;
}
export class DeleteReponse {
  response: boolean;
}
export class DeleteObject {
  response: boolean;
  message: string;
}
export class GeoZones {
  geoZoneId: string;
  countryId: string;
  governateId: string;
}
export interface DialogData {
  title: string;
  message: string;
}
// role
export class CreatRoleDto {
  code: string;
  nameAr: string;
  nameEn: string;
  description: string;
  defaultPageId: number;
  isActive: boolean;
  permissions: string[];
}
export class CreateResponse {
  statusCode: string;
  response: string;
  message: string;
  responseCode: number;
}
export class CreateAddressResponse {
  statusCode: string;
  response: AddressResponse;
  message: string;
  responseCode: number;
}
export class AddressResponse {
  patientAddressId: string;
  geoZoneId: string;
}
export class GetAttachmentResponse {
  response: string;
}
// visits
export class VisitListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  visits: VisitsListDto[];
}
export class VisitsListDto {
  visitId: string;
  visitNo: number;
  visitDate: Date;
  patientName: string;
  patientNo: number;
  genderName: string;
  patientMobileNo: number;
  statusName: string;
  chemistName: string;
  geoZoneName: string;
  timeSlot: string;
}
export class VisitsSearchCriteria {
  governateId: string;
  visitDateFrom: Date;
  visitDateTo: Date;
  visitNoFrom: number;
  pageSize: number;
  currentPageIndex: number;
  visitNoTo: number;
  creationDateFrom: Date;
  creationDateTo: Date;
  patientNo: string;
  patientName: string;
  gender: number;
  geoZoneId: string;
  phoneNumber: string;
  patientMobileNo: any;
  visitStatusTypeId: number;
  needExpert: boolean;
  sortBy: number;
  assignStatus: number;
  assignedTo: string;
  dob: string;
}
//Roles
export class RoleListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  roles: VisitsListDto[];
}
export class RoleListDto {
  code: number;
  name: string;
}
export class RolesSearchCriteria {
  code: number;
  name: string;
  isActive: boolean;
  pageSize: number;
  currentPageIndex: number;
  PhoneNumber?: string;
}
export class scheduleSearchCriteria {
  StartDate: string;
  EndDate: string;
  AssignedGeoZoneId: number;
  ChemistId: string;
}
export class UpdateRoleDto {
  roleId: string;
  name: string;
  description: string;
  isActive: boolean;
  defaultPageId: number;
  permissions: number[];
  geoZones: string[];
}
export class RoleViewResponse {
  response: RoleViewSecondResponse;
}
export class RoleViewSecondResponse {
  role: RoleViewDto;
}
export class RoleViewDto {
  roleId: string;
  code: number;
  name: string;
  description: string;
  isActive: boolean;
  defaultPageId: number;
  permissions: number[];
  geoZones: string[];
}
export class GetPatientVisitsResponse {
  response: GetPatientVisitsList[];
}
export class GetPatientVisitsList {
  visitId: string;
  visitNo: string;
  visitDate: Date;
  geoZoneId: string;
  visitStatusTypeId: number;
  patientId: string;
  zoneName: string;
  statusName: string;
  visitDateString: string;
  visitTime: string;
  patientPhoneNumbers: PendingVisits[];
  patientAddresses: PatientAddresses[];
}
export class GetAvailableVisitsInAreaResponse {
  response: GetAvailableVisitsInAreaList[];
}
export class GetAvailableVisitsInAreaList {
  geoZoneName: string;
  avalailableVisits: number;
  maxVisits: number;
  timeZoneName: string;
  timeZoneStartTime: string;
  timeZoneEndTime: string;
  timeZoneFrameGeoZoneId: string;
}
export class GetAllLostVisitTimesResponse {
  response: GetAllLostVisitTimes[];
}
export class GetAllLostVisitTimes {
  lostVisitTimeId: string;
  createdBy: string;
  visitId: string;
  createdOn: Date;
  visitNo: string;
  //waleed
}
export class GetAllAgeSegmentsResponse {
  response: GetAllAgeSegments[];
}
export class GetAllAgeSegments {
  ageSegmentId: string;
  name: string;
  ageFromDay: number;
  ageFromMonth: number;
  ageFromYear: number;
  ageToDay: number;
  ageToMonth: number;
  ageToYear: number;
}
export class CreateHoldVisit {
  timeZoneFrameGeoZoneId: string;
  noOfPatients: number;
  deviceSerialNo: string;
}
export class CreateSecondVisitByChemistApp {
  timeZoneGeoZoneId: string;
  visitDate: Date;
  requiredTests: string;
  comments: string;
  originVisitId: string;
  secondVisitReason: number;
  minMinutes: number;
  maxMinutes: number;
  longitude: number;
  latitude: number;
  deviceSerialNumber: string;
  mobileBatteryPercentage: number;
  chemistId: string;
}

export class CreatedVisitByChemistApp {
  visitTypeId: number;
  visitDate: string;
  patientId: string;
  patientAddressId: string;
  relativeAgeSegmentId: string;
  iamNotSure: boolean;
  relativeDateOfBirth: Date;
  timeZoneGeoZoneId: string;
  plannedNoOfPatients: number;
  requiredTests: string;
  comments: string;
  longitude: number;
  latitude: number;
  deviceSerialNumber: string;
  mobileBatteryPercentage: number;
  chemistId: string;
  visitTime: string;
}
export class CreateLostVisitTime {
  lostVisitTimeId: string;
  chemistId: string;
  lostTime: string;
  createdBy: string;
  createdOn: string;
}
export enum RoleStatus {
  'Active' = 'true',
  'Inactive' = 'false',
}
export enum ReasonsType {
  'Cancel Visit Type Action' = '4',
  'Reject visit Type Action' = '8',
  'request second visit Type Action' = '9',
  'Reassign visit Type Action' = '10',
}

//Permissions

export class GetPermissionsDTO {
  response: GetPermissionsParent;
}
export class GetUserPermissionsDTO {
  response: GetUserPermissionResponse;
}
export class GetUserPermissionResponse {
  systemPagePermissionDtos: SystemPagePermissionDto[]
}
export class SystemPagePermissionDto {
  systemPagePermissionId: number;
  systemPageId: number;
  permissionId: number;
}
export class GetPermissionsParent {
  systemPages: GetPermissionsChild[];
}
export class GetPermissionsChild {
  id: number;
  name: string;
  code: string;
  hasURL: boolean;
  position: number;
  isDisplayInMenue: boolean;
  parentId: number;
  children: GetPermissionsChild[]
  subChild: GetPermissionsChild[]
  permissions: PermissionsList[];
}
export class PermissionsList {
  systemPagePermissionId: number;
  permissionId: number;
  number: string;
  permissionCode: number;
}
export class systemPagesResponse {
  systemPages: systemPagesList[];
}
export class systemPagesList {
  id: number;
  name: string;
}
export class nodeList {
  nodeId: any;
  nodeText: string;
}
export class GetPermissionsUpdateDTO {
  response: GetPermissionsParentUpdate;
}
export class GetPermissionsParentUpdate {
  systemPages: GetPermissionsUIpdateChild[];
}
export class GetPermissionsUIpdateChild {
  id: number;
  name: string;
  permissions: GetPermissionsUIpdateChild[];
  isChecked: boolean;
}
export class PermissionsUpdateList {
  id: number;
  name: string;
}
export class SearchPatientsResponse {
  response: SearchPatientsList[];
}
export class SearchPatientsList {
  userId: string;
  name: string;
  gender: number;
  genderName: string;
  dob: string;
  birthDate: Date;
  phoneNumber: string;
  isTherePendingVisits: boolean;
  pendingVistis: PendingVisits[];
  patientPhoneNumbers: PendingVisits[];
  patientAddresses: PatientAddresses[];
}
export class PatientPhoneNumbers {
  visitId: string;
  visitNo: string;
  visitDate: Date;
  geoZoneId: string;
  visitStatusTypeId: number;
  patientId: string;
  zoneName: string;
  statusName: string;
  visitDateString: string;
  visitTime: string;
}
export class PendingVisits {
  phoneNumber: string;
  createdAt: Date;
}
export class PatientAddresses {
  patientAddressId: string;
  latitude: string;
  longitude: string;
  floor: string;
  flat: string;
  geoZoneId: string;
  building: string;
  street: string;
  isConfirmed: boolean;
  locationUrl: string;
  kmlFilePath: string;
  governateId: string;
  countryId: string;
  zoneName: string;
  governateName: string;
  countryName: string;
  addressCreatedAt: Date;
  addressFormatted: string;
}

export class SearchParentCriteria {
  phoneNumber: string;
}
export class SearchPatientsSendToParent {
  //patientId:string;
  isShow: boolean;
  patientData: SearchPatientsList;
}
export class ExportPatientData {
  patientId: string;
  relativeType: number;
  patientAddressId: string;
  isShowVisitData: boolean;
  geoZoneId: string;
  relativeAgeSegmentId: string;
  iamNotSure: boolean;
  relativeDateOfBirth: Date;
}
export class CreatePatientAddress {
  patientId: string;
  street: string;
  locationUrl: string;
  floor: string;
  flat: string;
  building: string;
  additionalInfo: string;
  geoZoneId: string;
  latitude: string;
  longitude: string;
}
export class ChemistListItem {
  response: Chemists[];
}
export class Chemists {
  chemistId: string;
  name: string;
}
export class CreatPatientDto {
  patientNo: string;
  dob: string;
  gender: number;
  name: string;
  birthDate: Date;
  addresses: CreatePatientAddress[];
  phones: Phones[];
}
export class Phones {
  phone: string;
}

export class SystemPage {
  id: number;
  text: string;
  hasChild: boolean;
  permissions: Permission[];
}
export class Permission {
  id: number;
  text: string;
  isChecked: boolean;
}

// export class ReasonsResponse
// {
//     response:ReasonsList[];
// }
// export class ReasonsList
// {
//     reasonActionId:number;
//     name:string;
// }

export class ReasonsPage {
  ReasonId: number;
  ReasonName: string;
  IsActive: boolean;
  VisitTypeActionId: number;
  PageSize: number;
  CurrentPageIndex: number;
}
export class ReasonsResponse {
  response: ReasonsList;
}
export class ReasonsList {
  reasons: ReasonsObj[];
}

export class ReasonsObj {
  reasonId: number;
  reasonName: string;
}
//areas
export class AreaSearchCriteria {
  code: number;
  name: string;
  countryId: string;
  pageSize: number;
  currentPageIndex: number;
  mappingCode: string;
  governateId: string;
  isActive: boolean;
}
export class AreaListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  geoZones: AreasListDto[];
}
export class AreasListDto {
  geoZoneId: string;
  code: number;
  geoZoneName: string;
  mappingCode: string;
  governateName: string;
  isActive: boolean;
}
export class CreateAreaDto {
  areaName: string;
  isActive: boolean;
  mappingCode: string;
  kmlFilePath: string;
  governateId: string;
  timeZoneFrames: TimeZoneFramesDto[];
}
export class UpdateAreaDto {
  code: number;
  name: string;
  isActive: boolean;
  mappingCode: string;
  kmlFilePath: string;
  governateId: string;
  timeZoneFrames: TimeZoneFramesDto[];
}

export class TimeZoneFramesDto {
  timeZoneFrameId: string;
  name: string;
  visitsNoQuota: number;
  startTime: string;
  endTime: string;
  branchDispatch: boolean;
  geoZoneId: string;
  maxNumVisitNo: number;
}
export class AreaViewResponse {
  response: AreaViewDto;
}

export class AreaViewDto {
  geoZoneId: string;
  code: number;
  geoZoneName: string;
  mappingCode: string;
  governateId: string;
  governateName: string;
  isActive: boolean;
  isDeleted: boolean;
  countryId: string;
  kmlFilePath: string;
  kmlFileName: string;
  timeZoneFrames: TimeZoneFramesDto[];
}
export class AgeSegmentItem {
  ageSegmentId: number;
  name: string;
}
export class AgeSegmentList {
  response: AgeSegmentItem[];
}
export class SendChemistAction {
  visitStatusId: string;
  visitId: string;
  longitude: number;
  latitude: number;
  deviceSerialNumber: string;
  mobileBatteryPercentage: number;
  visitActionTypeId: number;
  visitStatusTypeId: number;
  actualNoOfPatients: number;
  noOfTests: number;
  isAddressVerified: boolean;
  reasonId: number;
  comments: string;
  timeZoneGeoZoneId: string;
  visitDate: string;
  chemistId: string;
}
export class Attachment {
  fileName: string;
}
export class GetVisitDetailsResponse {
  response: GetVisitDetails;
}
export class GetVisitDetails {
  visitId: string;
  visitNo: string;
  visitDate: Date;
  createdBy: string;
  patientId: string;
  name: string;
  dob: string;
  gender: number;
  genderName: string;
  requiredTests: string;
  comments: string;
  geoZoneId: string;
  zoneName: string;
  visitStatusTypeId: number;
  statusName: string;
  visitDateString: string;
  visitTime: string;
  actionCreationDate: Date;
  plannedNoOfPatients: number;
  patientMobileNumber: string;
  latitude: number;
  reservedBy: string;
  floor: string;
  flat: string;
  building: string;
  street: string;
  addressFormatted: string;
  goverName: string;
  visitAttachments: visitAttachments[];
  timeZoneGeoZoneId: string;
  startTime: string;
  endTime: string;
  visitStatuses: visitStatuses[];
  visitTypeId: number;
  relativeAgeSegmentId: string;
}
export class visitStatuses {
  creationDate: Date;
  status: string;
  userName: string;
  longitude: number;
  latitude: number;
  visitId: string;
}
export class visitAttachments {
  attachmentId: string;
  fileName: string;
  visitId: string;
}
export enum AssignedStatus {
  'Assigned' = 1,
  'Not Assigned' = 2,
}
export enum VisitStatus {
  'New' = 1,
  'Confirmed' = 2,
  'Done' = 3,
  'Cancelled' = 4,
  'ToCustomer' = 5,
  'Arrived' = 6,
  'Accept' = 7,
  'Reject' = 8,
}
export enum SortBy {
  'Visit Date Ascending' = 1,
  'Creation Date Ascending' = 3,
  'Creation Date Descending' = 4,
}
//Query Time
export class QueryTimesListResponse {
  currentPageIndex: number;
  pageSize: number;
  totalCount: number;
  times: QueryTimesListDTO[];
}
export class QueryTimesListDTO { }
export class QueryTimeCriteria {
  governateId: string;
  countryId: string;
  pageSize: number;
  currentPageIndex: number;
  geoZoneId: string;
}
export class PatientViewResponse {
  response: PatientViewDto;
  responseCode: number;
}

export class PatientViewDto {
  userId: string;
  name: string;
  gender: number;
  genderName: string;
  dob: string;
  birthDate: Date;
  phoneNumber: string;
  isTherePendingVisits: boolean;
  governateName: string;
  pendingVistis: PendingVisits[];
  patientPhoneNumbers: PendingVisits[];
  patientAddresses: PatientAddresses[];
}
