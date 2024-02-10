
import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ReportsService {
    visitSubject$: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    reportsUrl = EndPoints.REPORTS_ENDPOINT

    constructor(private http: HttpClient, private datepipe: DatePipe) { }

    getVisitesRes(data) {
        this.visitSubject$.next(data)
    }

    getVisitReoprts(body) {

        let params = new HttpParams();
        params = body

        return this.http.get<any>(`${this.reportsUrl}GetVisitReport/Total`, { params })
    }
    getVisitReoprtDitailed(body) {
        let params = new HttpParams();

        params = body

        return this.http.get<any>(`${this.reportsUrl}GetVisitReport/Detailed`, { params })
    }
    getReassignedReport(body) {

        let params = new HttpParams();
        params = body

        return this.http.get<any>(`${this.reportsUrl}GetReassignedReport`, { params })
    }
    getCanceledVisitReport(body) {
        let params = new HttpParams();
        params = body


        return this.http.get<any>(`${this.reportsUrl}GetCanceledVisitReport`, { params })
    }
    getVisitReject(body) {
        let params = new HttpParams();
        params = body


        return this.http.get<any>(`${this.reportsUrl}GetRejectedVisitReport`, { params })
    }
    getLostBusinessReport(body) {
        let params = new HttpParams();
        params = body
        return this.http.get<any>(`${this.reportsUrl}GetLostBusinessReport`, { params })
    }
    getTATTotalReport(body) {
        let params = new HttpParams();
        params = body
        return this.http.get<any>(`${this.reportsUrl}GetTATTotalReport`, { params })
    }

    getTATDetailsReport(body) {
        let params = new HttpParams();
        params = body
        return this.http.get<any>(`${this.reportsUrl}GetTATDetailsReport`, { params })
    }
}
