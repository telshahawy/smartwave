
import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class SystemParametersService {

    systemParUrl = EndPoints.SYSTEMPARAMETERS_ENDPOINT

    constructor(private http: HttpClient) { }

    getById() {
        return this.http.get<any>(`${this.systemParUrl}/GetSystemParameterByClientId`)
    }



    edit(body) {
        return this.http.put<any>(`${this.systemParUrl}`, body)

    }


    create(body) {
        return this.http.post<any>(`${this.systemParUrl}`, body)

    }

    upload(body) {
        return this.http.post<any>(`${this.systemParUrl}/UploadPrecautionsFile`, body)

    }
    dateVisit() {
        return this.http.get<any>(`${this.systemParUrl}/GetVisitDatesRegardingSysParam`)
    }
    GetVisitAcceptCancelPermission(body) {
        let params = new HttpParams();
        params = body
        return this.http.get<any>(`${this.systemParUrl}/GetVisitAcceptCancelPermission`,{params})
    }

    

}
