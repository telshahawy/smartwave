import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AccountService } from '../data-services/account.service';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { NotifyService } from '../data-services/notify.service';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  authUrl: string = environment.authUrl;
  constructor(public accountService: AccountService ,
    private notifyService: NotifyService,
    private router:Router) { }
  // this.accountService.getAccessToken()}`,
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.search(this.authUrl) !== -1) {
      return next.handle(request);
    }

    const cloned = request.clone({
      headers: request.headers.set("Authorization",
          "Bearer " + this.accountService.getAccessToken())
  });

  return next.handle(cloned).pipe(
      catchError((error: HttpErrorResponse) => {
        

          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
              // client-side error
              errorMessage = `Error: ${error.message}`;

          }
           else {
              // server-side error
              errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
              if (error.status == 401) {
                 
                  this.router.navigate(["/"]);
                  this.notifyService.error( error.status,'FAILED OPERATION');

              }
              // if (error.status == 400) {
              //     this.notifyService.error( error.statusText,'FAILED OPERATION');

              // }
              if (error.status == 422) {
                  this.notifyService.error( error.status,'FAILED OPERATION');

              }
              // if (error.status == 500) {
              //     this.notifyService.error( error.status,'FAILED OPERATION');

              // }
              if(error.status ==404){
                this.notifyService.error( error?.status,'FAILED OPERATION');

              }



          }
          return throwError(error.error);

      })
  );

  }
}