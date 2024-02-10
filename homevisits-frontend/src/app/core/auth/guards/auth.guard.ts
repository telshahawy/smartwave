import { Injectable } from '@angular/core';
import {  ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { OAuthService, UserInfo } from 'angular-oauth2-oidc';

@Injectable()
export class AuthGuard implements CanActivate {
  userProfile: UserInfo;
  constructor(private router: Router, private oauthService: OAuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot, ) {
    const client: string = route.params.Client;
    sessionStorage.setItem('requestedUrl', state.url);
    this.oauthService.events;
    if (
      this.oauthService.hasValidAccessToken() &&
      this.oauthService.hasValidIdToken()
      // && this.userProfile['UserType']==3
    ) {
      return true;
    } else {
      // if (client === undefined) {
         this.router.navigate(['/autologin']);
      // }
      // else {
      //   this.router.navigate(["/" + client + '/autologin']);
      // }

         return false;
    }
  }
}
