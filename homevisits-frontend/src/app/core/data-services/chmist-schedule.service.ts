
import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { IpagedList } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ChemistScheduleService {
    // , private translate: TranslateService

    chemistScheduleUrl = EndPoints.CHEMIST_ENDPOINT


    constructor(private http: HttpClient, private datepipe: DatePipe) { }

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
    getById(id) {
        return this.http.get<any>(`${this.chemistScheduleUrl}/GetChemistScheduleForEdit` + '?' + 'ChemistScheduleId=' + id)
    }

    delete(id) {
        return this.http.delete<any>(`${this.chemistScheduleUrl}/DeleteChemistSchedule` + '?' + 'ChemistScheduleId=' + id)
    }

    edit(body, id) {
        return this.http.put<any>(`${this.chemistScheduleUrl}/UpdateChemistSchedule` + '?' + 'ChemistScheduleId=' + id, body)

    }
    duplicate(body){
        return this.http.post<any>(`${this.chemistScheduleUrl}/DuplicateChemistSchedule`,body)
    }

    search(body) {
        return this.http.get<IpagedList<any>>(`${this.chemistScheduleUrl}/SearchChemistSchedule` +
            '?' +
            this.toQueryString(body as any)
        );
    }
    create(schdeuleData) {
        return this.http.post<any>(`${this.chemistScheduleUrl}/CreateChemistSchedule`,
            schdeuleData
        );
    }


}
