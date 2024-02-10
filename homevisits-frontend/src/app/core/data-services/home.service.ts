
import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class HomeService {

    homeUrl=EndPoints.HOME_ENDPOINT;
    


    constructor(private http: HttpClient) { }

    getHomeStat(geozoneId) {
        return this.http.get<any>(`${this.homeUrl}GetHomePageStatistics?geozoneId=${geozoneId}`)
    }
    getActiveChemist() {
        return this.http.get<any>(`${this.homeUrl}GetActiveChemists`)
    }
    getIdleChemists() {
        return this.http.get<any>(`${this.homeUrl}GetIdleChemists`)
    }
    getAreaByUser() {
        return this.http.get<any>(`${this.homeUrl}GetAreasByUserId`)
    }
    getTotalVisit() {
        return this.http.get<any>(`${this.homeUrl}GetTotalVisitsList`)
    }
    getDelyedVisit() {
        return this.http.get<any>(`${this.homeUrl}GetDelayedVisitsList`)
    }
    getPendingVisit() {
        return this.http.get<any>(`${this.homeUrl}GetPendingVisitsList`)
    }
    getRessaginedVisit() {
        return this.http.get<any>(`${this.homeUrl}GetReassignedVisitsList`)
    }



}
