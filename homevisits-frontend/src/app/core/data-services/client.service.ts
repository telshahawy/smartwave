import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Injectable, Injector } from '@angular/core';
import { createElementParams } from '@syncfusion/ej2-angular-inputs';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import {
  AgeSegmentList,
  AreaList,
  AreaListResponse,
  AreaLookup,
  AreaSearchCriteria,
  AreaViewResponse,
  ChemistCreateDto,
  ChemistLastTrackingLogListResponse,
  ChemistListItem,
  ChemistListResponse,
  ChemistVisitsScheduleModel,
  ChemistsListDto,
  ChemistsSearchCriteria,
  ChemistUpdateDto,
  ChemistViewDto,
  ChemistViewResponse,
  CountryList,
  CreateAddressResponse,
  CreateAreaDto,
  CreatedVisitByChemistApp,
  CreateHoldVisit,
  CreateLostVisitTime,
  CreatePatientAddress,
  CreateResponse,
  CreateSecondVisitByChemistApp,
  CreatRoleDto,
  DeleteObject,
  DeleteReponse,
  GetAllAgeSegments,
  GetAllAgeSegmentsResponse,
  GetAllLostVisitTimes,
  GetAllLostVisitTimesResponse,
  GetAttachmentResponse,
  GetAvailableVisitsInAreaList,
  GetAvailableVisitsInAreaResponse,
  GetChemistsLastTracking,
  GetPatientVisitsResponse,
  GetPermissionsDTO,
  GetPermissionsParentUpdate,
  GetPermissionsUIpdateChild,
  GetPermissionsUpdateDTO,
  GetVisitDetailsResponse,
  GovernateList,
  IpagedList,
  PatientViewResponse,
  ReasonsPage,
  ReasonsResponse,
  RoleListResponse,
  RolesSearchCriteria,
  RoleViewResponse,
  SearchParentCriteria,
  SearchPatientsList,
  SearchPatientsResponse,
  SendChemistAction,
  systemPagesResponse,
  UpdateAreaDto,
  UpdateReponse,
  UpdateRoleDto,
  VisitListResponse,
  VisitsSearchCriteria,
  ChemistVisitsScheduleListResponse,
  ChemistPermitSearchCriteria,
  ChemistPermitListResponse,
  CreateChemistPermitModel,
  ChemistPermitViewResponse,
  UpdateChemistPermitModel,
  GetUserPermissionsDTO,
} from '../models/models';
import { AgeSegmentService } from './ageSegments.service';
import { ChemistScheduleService } from './chmist-schedule.service';
import { HomeService } from './home.service';

import { ReasonsService } from './reasons.service';
import { ReportsService } from './reports.service';
import { SystemParametersService } from './systemparameters.service';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  private chemistId: BehaviorSubject<string> = new BehaviorSubject<string>('');
  baseUrl: string = environment.baseUrl;
  searchChemistsClientUrl = 'api/Chemist/SearchChemists';
  createChemistsClientUrl = 'api/Chemist/CreateChemist';
  GetChemistsLastTrackingClientUrl = 'api/Chemist/GetChemistsLastTracking';
  GetChemistsVisitsScheduleById = 'api/Schedule/ChemistScheduleById';
  searchChemistPermitssClientUrl = 'api/Chemist/SearchChemistPermits';
  createChemistPermitsClientUrl = 'api/Chemist/CreateChemistPermit';
  updateChemistPermitsClientUrl = 'api/Chemist/UpdateChemistPermit';
  getCountriesClientUrl = 'api/Countries/CountriesKeyValue';
  getCountriesListDataUrl = 'api/Countries/SearchCountries';
  getAreasClientUrl = 'api/GeoZones/GeoZonesKeyValue';
  getGovernatsClientUrl = 'api/Governats/GovernatsKeyValue';
  getGovernatsSearchUrl = 'api/Governats/SearchGovernats';
  deleteChemistUrl = 'api/Chemist/DeleteChemist';
  deleteChemistPermitsUrl = 'api/Chemist/DeleteChemistPermit';
  updateChemistUrl = 'api/Chemist/UpdateChemist';
  getUserData = 'api/ClientUsers/GetClientUserForEdit?UserId=';
  getChemistUrl = 'api/Chemist/GetChemistForEdit';
  getChemistPermitUrl = 'api/Chemist/GetChemistPermitForEdit';
  createRoleUrl = 'api/Roles/CreateRole';
  createUserUrl = 'api/ClientUsers/CreateClientUser';
  updateUserUrl = 'api/ClientUsers/UpdateClientUser';
  createScheduleUrl = 'api/Chemist/CreateChemistSchedule';
  deleteRoleUrl = 'api/Roles/DeleteRole';
  deleteCountryUrl = 'api/Countries/DeleteCountry';
  deleteGoverUrl = 'api/Governats/DeleteGovernate';
  deleteUserUrl = 'api/ClientUsers/DeleteClientUser';
  updateRoleUrl = 'api/Roles/UpdateRole';
  getRoleUrl = 'api/Roles/GetRoleForEdit';
  getAttachmentUrl = 'api/Attachments/UpoadUserImage';
  searchVisitsClientUrl = 'api/Visit/SearchVisits';
  searchRolesUrl = 'api/Roles/SearchRoles';
  searchUsersUrl = 'api/ClientUsers/SearchClientUsers';
  searchScheduleUrl = 'api/Chemist/SearchChemistSchedule';
  GeoChemistZonesKeyValue = 'api/Chemist/GetChemistAssignedGeoZonesKeyValue';
  getPermissionsUrl = 'api/SystemPages/GetSystemPagesTree';
  GetUserSystemPageWithPermissionsTreeUrl =
    'api/ClientUsers/GetUserSystemPageWithPermissionsTree';
  GetUserPermissionUrl = 'api/ClientUsers/GetUserPermission';
  getSegmentsClientUrl = 'api/Visit/GetAllAgeSegments';
  getPatientVisitsByPatienUrl = 'api/Visit/GetPatientVisitsByPatientId';
  getSecondVisitTimeZoneAndDateUrl = 'api/Visit/GetSecondVisitTimeZoneAndDate';
  getAvailableVisitsInAreaUrl = 'api/Visit/GetAvailableVisitsInAreaWeb';
  getVisitDetailsByVisitUrl = 'api/Visit/GetVisitDetailsByVisitId';
  getAllLostVisitTimesUrl = 'api/Visit/GetAllLostVisitTimes';
  getAllAgeSegmentsUrl = 'api/Visit/GetAllAgeSegments';
  createLostVisitTimeUrl = 'api/Visit/CreateLostVisitTime';
  createVisitByChemistAppUrl = 'api/Visit/AddVisitByChemistApp';
  createSecondVisitByChemistAppUrl = 'api/Visit/AddSecondVisitByChemistApp';
  holdVisitUrl = 'api/Visit/HoldVisit';
  systemPageListUrl = 'api/SystemPages/GetSystemPagesKeyValue';
  systemPagesTreeListUrl = 'api/SystemPages/SystemPages';
  searchPatientsUrl = 'api/Visit/SearchPatients';
  createPatientAddressUrl = 'api/Patients/AddPatientAddress';
  getChemistListItemUrl = 'api/Chemist/ChemistsKeyValue';
  createPatientUrl = 'api/Patients/AddPatient';
  getReasonsUrl = 'api/Reasons/GetReasonActionsKeyValue';

  createCountryUrl = 'api/Countries/CreateCountry';
  updateCountryUrl = 'api/Countries/UpdateCountry';
  getCountryUrl = 'api/Countries/GetCountryForEdit';
  createGoverUrl = 'api/Governats/CreateGovernate';
  updateGoverUrl = 'api/Governats/UpdateGovernate';
  getGoverUrl = 'api/Governats/GetGovernateForEdit';
  //Areas
  searchAreasClientUrl = 'api/GeoZones/SearchGeoZones';
  createAreaClientUrl = 'api/GeoZones/AddGeoZone';
  deleteAreaUrl = 'api/GeoZones/DeleteGeoZone';
  updateAreaUrl = 'api/GeoZones/UpdateGeoZone';
  getAreaUrl = 'api/GeoZones/GetGeoZoneForEdit';
  uploadFileUrl = 'api/Attachments/UpoadVisitData';
  uploadKmlFileUrl = 'api/Attachments/UpoadKmlFile';
  sendChemistActionUrl = 'api/Schedule/SendChemistAction';
  downloadKmlFileUrl = 'api/Attachments/DownloadKML';
  searchReasonByActionUrl = 'api/Reasons/SearchReasons';
  getAvailableVisitsForChemistUrl = 'api/Visit/GetAvailableVisitsForChemist';
  getPatientUrl = 'api/Patients/GetPatientForEdit';
  // backUrl:string;
  constructor(
    private http: HttpClient,
    private datepipe: DatePipe,
    private injector: Injector
  ) {
    // this.backUrl=this.enviroment.ba
  }

  // REASONS SERVICE INJECTION
  private _reasonsService: ReasonsService;
  private _ageSegmentsService: AgeSegmentService;
  private _chemistScheduleService: ChemistScheduleService;
  private _systemParametersService: SystemParametersService;
  private _homeService: HomeService;
  private _reportsService: ReportsService;

  get reportsService(): ReportsService {
    if (!this._reportsService) {
      this._reportsService = this.injector.get(ReportsService);
    }

    return this._reportsService;
  }
  get homeService(): HomeService {
    if (!this._homeService) {
      this._homeService = this.injector.get(HomeService);
    }

    return this._homeService;
  }
  get reasonsService(): ReasonsService {
    if (!this._reasonsService) {
      this._reasonsService = this.injector.get(ReasonsService);
    }

    return this._reasonsService;
  }
  get ageSegmentsService(): AgeSegmentService {
    if (!this._ageSegmentsService) {
      this._ageSegmentsService = this.injector.get(AgeSegmentService);
    }

    return this._ageSegmentsService;
  }
  get chemistScheduleService(): ChemistScheduleService {
    if (!this._chemistScheduleService) {
      this._chemistScheduleService = this.injector.get(ChemistScheduleService);
    }

    return this._chemistScheduleService;
  }
  get systemParameters(): SystemParametersService {
    if (!this._systemParametersService) {
      this._systemParametersService = this.injector.get(
        SystemParametersService
      );
    }

    return this._systemParametersService;
  }

  getSecondVisitTimeZoneAndDate(body): Observable<any> {
    return this.http.get(this.baseUrl + this.getSecondVisitTimeZoneAndDateUrl, {
      params: body,
    });
  }

  toQueryString(obj, prefix?) {
    let str = [],
      k,
      v;
    for (const p in obj) {
      if (!obj.hasOwnProperty(p)) {
        continue;
      } // skip things from the prototype
      if (~p.indexOf('[')) {
        k = prefix ? prefix + '.' + p : p;
        // only put whatever is before the bracket into new brackets; append the rest
      } else {
        k = prefix ? prefix + '.' + p : p;
      }
      v = obj[p];
      if (v instanceof Date) v = this.datepipe.transform(v, 'yyyy-MM-dd ');
      if (typeof v != 'string' && v && v.length > 0) {
        v.forEach((el) => {
          str.push(
            typeof el == 'object'
              ? this.toQueryString(el, k)
              : k + '=' + encodeURIComponent(el)
          );
        });
      } else {
        str.push(
          typeof v == 'object'
            ? this.toQueryString(v, k)
            : k + '=' + encodeURIComponent(v)
        );
      }
    }
    return str.join('&');
  }
  getchemistId() {
    return this.chemistId.asObservable();
  }
  createPatientPhone(body) {
    return this.http.post(this.baseUrl + 'api/Patients/AddPatientPhone', body);
  }
  updatechemistId(chemistIdd: string) {
    this.chemistId.next(chemistIdd);
  }
  deleteChemistPermit(chemistPermitId: string) {
    return this.http.delete<UpdateReponse>(
      this.baseUrl + this.deleteChemistPermitsUrl + '?' + 'chemistPermitId=' + chemistPermitId
    );
  }
  searchChemistPermits(body: ChemistPermitSearchCriteria) {
    return this.http.get<IpagedList<ChemistPermitListResponse>>(
      this.baseUrl +
        this.searchChemistPermitssClientUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  createChemistPermits(body: CreateChemistPermitModel){
    
    return this.http.post<CreateChemistPermitModel>(
      this.baseUrl + this.createChemistPermitsClientUrl,
      body
    );
  }
  searchChemists(body: ChemistsSearchCriteria) {
    return this.http.get<IpagedList<ChemistListResponse>>(
      this.baseUrl +
        this.searchChemistsClientUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }

  GetChemistScheduleById(chemistId: string) {
    return this.http.get<ChemistVisitsScheduleListResponse>(
      this.baseUrl +
        this.GetChemistsVisitsScheduleById +
        '?chemistId=' +
        chemistId
    );
  }
  getChemistsLastTracking(body: GetChemistsLastTracking) {
    return this.http.get<IpagedList<ChemistLastTrackingLogListResponse>>(
      this.baseUrl +
        this.GetChemistsLastTrackingClientUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  createChemist(dto: ChemistCreateDto) {
    return this.http.post<ChemistCreateDto>(
      this.baseUrl + this.createChemistsClientUrl,
      dto
    );
  }
  getCountries() {
    return this.http.get<IpagedList<CountryList>>(
      this.baseUrl + this.getCountriesClientUrl
    );
  }
  searchCountries(body) {
    return this.http.get<IpagedList<any>>(
      this.baseUrl +
        this.getCountriesListDataUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  searchgovers(body) {
    return this.http.get<IpagedList<any>>(
      this.baseUrl +
        this.getGovernatsSearchUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  getAreas(id?: string) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { GovernateId: id ? id : '' },
    };
    return this.http.get<IpagedList<AreaList>>(
      this.baseUrl + this.getAreasClientUrl,
      httpOptions
    );
  }
  getGovernats(id?: string) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { CountryId: id ? id : '' },
    };

    return this.http.get<IpagedList<GovernateList>>(
      this.baseUrl + this.getGovernatsClientUrl,
      httpOptions
    );
  }

  getChemist(id: string) {
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { ChemistId: id },
    };
    return this.http.get<ChemistViewResponse>(
      this.baseUrl + this.getChemistUrl,
      httpOptions
    );
  }
  getChemistPermit(id: string) {
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { ChemistPermitId: id },
    };
    return this.http.get<ChemistPermitViewResponse>(
      this.baseUrl + this.getChemistPermitUrl,
      httpOptions
    );
  }
  getUser(id: string) {
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
    };
    return this.http.get<any>(
      this.baseUrl + this.getUserData + id,
      httpOptions
    );
  }

  updateChemist(dto: ChemistUpdateDto) {
    return this.http.post<UpdateReponse>(
      this.baseUrl + this.updateChemistUrl,
      dto
    );
  }
  updateChemistPermit(dto: UpdateChemistPermitModel, chemistPermitId : string) {
    return this.http.put<UpdateReponse>(
      this.baseUrl + this.updateChemistPermitsClientUrl +'?chemistPermitId=' + chemistPermitId,
      dto
    );
  }
  deleteChemist(id: string) {
    return this.http.delete<UpdateReponse>(
      this.baseUrl + this.deleteChemistUrl + '?' + 'ChemistId=' + id
    );
  }
  //role
  createRole(dto: CreatRoleDto) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createRoleUrl,
      dto
    );
  }
  getRole(id: string) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { roleId: id },
    };
    return this.http.get<RoleViewResponse>(
      this.baseUrl + this.getRoleUrl,
      httpOptions
    );
  }
  createUser(dto) {
    return this.http.post<any>(this.baseUrl + this.createUserUrl, dto);
  }
  UpdateUser(dto, userId) {
    return this.http.put<any>(
      this.baseUrl + this.updateUserUrl + '?UserId=' + userId,
      dto
    );
  }
  getRoleById(id: string) {
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { roleId: id },
    };
    return this.http.get<RoleViewResponse>(
      this.baseUrl + this.getRoleUrl,
      httpOptions
    );
  }
  updateRole(dto: UpdateRoleDto) {
    return this.http.put<UpdateReponse>(
      this.baseUrl + this.updateRoleUrl + '?' + 'roleId=' + dto.roleId,
      dto
    );
  }
  deleteRole(id: string) {
    return this.http.delete<DeleteReponse>(
      this.baseUrl + this.deleteRoleUrl + '?' + 'roleId=' + id
    );
  }
  deleteGover(id: string) {
    return this.http.delete<DeleteReponse>(
      this.baseUrl + this.deleteGoverUrl + '?' + 'governateId=' + id
    );
  }
  deleteCountry(id: string) {
    return this.http.delete<DeleteReponse>(
      this.baseUrl + this.deleteCountryUrl + '?' + 'countryId=' + id
    );
  }
  deleteUser(id: string) {
    return this.http.delete<DeleteReponse>(
      this.baseUrl + this.deleteUserUrl + '?' + 'userId=' + id
    );
  }
  getAttachment(data: any) {
    return this.http.post<GetAttachmentResponse>(
      this.baseUrl + this.getAttachmentUrl,
      data
    );
  }
  searchVisits(body: VisitsSearchCriteria) {
    return this.http.get<IpagedList<VisitListResponse>>(
      this.baseUrl +
        this.searchVisitsClientUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  searchRole(body: RolesSearchCriteria) {
    return this.http.get<IpagedList<RoleListResponse>>(
      this.baseUrl + this.searchRolesUrl + '?' + this.toQueryString(body as any)
    );
  }
  searchUser(body) {
    return this.http.get<any>(
      this.baseUrl + this.searchUsersUrl + '?' + this.toQueryString(body as any)
    );
  }

  GETGeoChemistZonesKeyValue(CHECMISTID) {
    return this.http.get<any>(
      this.baseUrl + this.GeoChemistZonesKeyValue + '?chemistId=' + CHECMISTID
    );
  }
  //visits
  GetPatientVisitsByPatientId(id: string) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { PatientId: id },
    };
    return this.http.get<GetPatientVisitsResponse>(
      this.baseUrl + this.getPatientVisitsByPatienUrl,
      httpOptions
    );
  }
  GetAvailableVisitsInArea(id: string, date: any) {
    let params = new HttpParams()
      .set('GeoZoneId', id)
      .set('Date', date.toString());
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: params,
    };
    return this.http.get<GetAvailableVisitsInAreaResponse>(
      this.baseUrl + this.getAvailableVisitsInAreaUrl,
      httpOptions
    );
  }
  GetAllLostVisitTimes() {
    return this.http.get<GetAllLostVisitTimesResponse>(
      this.baseUrl + this.getAllLostVisitTimesUrl
    );
  }
  GetAllAgeSegments() {
    return this.http.get<GetAllAgeSegmentsResponse>(
      this.baseUrl + this.getAllAgeSegmentsUrl
    );
  }
  createLostVisitTime(dto: CreateLostVisitTime) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createLostVisitTimeUrl,
      dto
    );
  }
  createVisitByChemistApp(dto: CreatedVisitByChemistApp) {
    
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createVisitByChemistAppUrl,
      dto
    );
  }
  createSecondVisitByChemistApp(dto: CreateSecondVisitByChemistApp) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createSecondVisitByChemistAppUrl,
      dto
    );
  }

  createholdVisit(dto: any) {
    return this.http.post<any>(this.baseUrl + this.holdVisitUrl, dto);
  }
  GetUserPermissions(userId: string) {
    return this.http.get<GetPermissionsDTO>(
      this.baseUrl + this.GetUserSystemPageWithPermissionsTreeUrl + '?userId=' + userId
    );
  }
  GetUserPermissionForEdit(userId: string) {
    return this.http.get<GetUserPermissionsDTO>(
      this.baseUrl + this.GetUserPermissionUrl + '?userId=' + userId
    );
  }
  GetPermissions() {
    return this.http.get<GetPermissionsDTO>(
      this.baseUrl + this.getPermissionsUrl
    );
  }
  GetPermissionsForUpdate() {
    return this.http.get<GetPermissionsUpdateDTO>(
      this.baseUrl + this.getPermissionsUrl
    );
  }
  getSystemPages() {
    return this.http.get<IpagedList<systemPagesResponse>>(
      this.baseUrl + this.systemPageListUrl
    );
  }
  getSystemPagesTree(){

  }
  searchPatients(body: SearchParentCriteria) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { PhoneNumber: body.phoneNumber },
    };
    return this.http.get<SearchPatientsResponse>(
      this.baseUrl + this.searchPatientsUrl,
      httpOptions
    );
  }
  createPatientAddress(dto: CreatePatientAddress) {
    return this.http.post<CreateAddressResponse>(
      this.baseUrl + this.createPatientAddressUrl,
      dto
    );
  }
  createPatient(dto: CreatRoleDto) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createPatientUrl,
      dto
    );
  }
  createCountry(dto) {
    return this.http.post<any>(this.baseUrl + this.createCountryUrl, dto);
  }
  getCountry(countryId) {
    return this.http.get<any>(
      this.baseUrl + this.getCountryUrl + '?countryId=' + countryId
    );
  }
  updateCountry(dto, countryId) {
    return this.http.put<any>(
      this.baseUrl + this.updateCountryUrl + '?countryId=' + countryId,
      dto
    );
  }

  createGover(dto) {
    return this.http.post<any>(this.baseUrl + this.createGoverUrl, dto);
  }
  getGover(GoverId) {
    return this.http.get<any>(
      this.baseUrl + this.getGoverUrl + '?governateId=' + GoverId
    );
  }
  updateGover(dto, GoverId) {
    return this.http.put<any>(
      this.baseUrl + this.updateGoverUrl + '?governateId=' + GoverId,
      dto
    );
  }

  getChemistListItem(GeoZoneId: string) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { GeoZoneId: GeoZoneId },
    };
    return this.http.get<ChemistListItem>(
      this.baseUrl + this.getChemistListItemUrl,
      httpOptions
    );
  }
  getReasons() {
    return this.http.get<ReasonsResponse>(this.baseUrl + this.getReasonsUrl);
  }
  //Areas
  getArea(id: string) {
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { geoZoneId: id },
    };
    return this.http.get<AreaViewResponse>(
      this.baseUrl + this.getAreaUrl,
      httpOptions
    );
  }

  updateArea(dto: UpdateAreaDto) {
    return this.http.put<UpdateReponse>(this.baseUrl + this.updateAreaUrl, dto);
  }
  searchAreas(body: AreaSearchCriteria) {
    return this.http.get<IpagedList<AreaListResponse>>(
      this.baseUrl +
        this.searchAreasClientUrl +
        '?' +
        this.toQueryString(body as any)
    );
  }
  createArea(dto: CreateAreaDto) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.createAreaClientUrl,
      dto
    );
  }
  deleteArea(id: string) {
    return this.http.delete<DeleteObject>(
      this.baseUrl + this.deleteAreaUrl + '?' + 'geoZoneId=' + id
    );
  }
  getAllAgeSegments() {
    return this.http.get<IpagedList<AgeSegmentList>>(
      this.baseUrl + this.getSegmentsClientUrl
    );
  }
  uplodaVisitFile(data: any) {
    return this.http.post<GetAttachmentResponse>(
      this.baseUrl + this.uploadFileUrl,
      data
    );
  }
  searchReasonByAction(visitType: any) {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { VisitTypeActionId: visitType },
    };
    return this.http.get<ReasonsResponse>(
      this.baseUrl + this.searchReasonByActionUrl,
      httpOptions
    );
  }
  sendChemistAction(dto: SendChemistAction) {
    return this.http.post<CreateResponse>(
      this.baseUrl + this.sendChemistActionUrl,
      dto
    );
  }
  downloadKmlFile(fileName) {
    let HTTPOptions: Object = {
      responseType: 'blob',
    };
    return this.http.post<any>(
      this.baseUrl + this.downloadKmlFileUrl + '?fileName=' + fileName,
      {},
      HTTPOptions
    );
  }
  sendApproveVisit(body) {
    return this.http.post<any>(this.baseUrl + this.sendChemistActionUrl, body);
  }
  uplodaKlmFile(data: any) {
    return this.http.post<GetAttachmentResponse>(
      this.baseUrl + this.uploadKmlFileUrl,
      data
    );
  }
  GetAvailableVisitsForChemist(chemistId: string, id: string, date: any) {
    let params = new HttpParams()
      .set('GeoZoneId', id)
      .set('Date', date.toString())
      .set('ChemistId', chemistId);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: params,
    };
    return this.http.get<GetAvailableVisitsInAreaResponse>(
      this.baseUrl + this.getAvailableVisitsForChemistUrl,
      httpOptions
    );
  }
  GetVisitDetailsById(id: string) {
    let params = new HttpParams().set('VisitId', id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: params,
    };
    return this.http.get<GetVisitDetailsResponse>(
      this.baseUrl + this.getVisitDetailsByVisitUrl,
      httpOptions
    );
  }
  getPatient(id: string) {
    
    //let params = new HttpParams().set("ChemistId",id);
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { patientId: id },
    };
    return this.http.get<PatientViewResponse>(
      this.baseUrl + this.getPatientUrl,
      httpOptions
    );
  }
}
