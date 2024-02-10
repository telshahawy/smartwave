
import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Injectable({
    providedIn: 'root'
})
export class ReasonsService {
    // , private translate: TranslateService

    searchReasonUrl = EndPoints.SEARCH_REASONS_ENDPOINT
    updateReasonUrl = EndPoints.UPDATE_REASONS_ENDPOINT
    createReasonUrl = EndPoints.CREATE_REASONS_ENDPOINT
    deleteReasonUrl = EndPoints.DELETE_REASONS_ENDPOINT
    getForEditReasonUrl = EndPoints.GET_FOR_EDIT_REASONS_ENDPOINT
    getReasonActionValueUrl = EndPoints.GET_REASONS_ACTIONS_KEY_VALUE_REASONS_ENDPOINT

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
      return  this.http.get<any>(this.getForEditReasonUrl + '?' + 'reasonId='+id )
    }

    delete(id) {
        return this.http.delete<any>(this.deleteReasonUrl + '?' + 'reasonId='+id)
    }

    edit(body,id) {
        return this.http.put<any>(this.updateReasonUrl+ '?' + 'reasonId='+id, body)

    }

    search(body) {
        return this.http.get<any>(this.searchReasonUrl + '?' + this.toQueryString(body as any))
    }

    create(body) {
        return this.http.post<any>(this.createReasonUrl, body)

    }

    getActionReasons() {
        return this.http.get<any>(this.getReasonActionValueUrl)

    }
}
