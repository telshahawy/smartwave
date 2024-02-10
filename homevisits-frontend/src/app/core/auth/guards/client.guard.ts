import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { OAuthService, UserInfo } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { AccountService } from '../../data-services/account.service';

@Injectable({
  providedIn: 'root'
})
export class ClientGuard implements CanActivate {

  constructor(private router: Router, private oauthService: OAuthService, private accountService: AccountService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot, ) {
    const client: string = route.params.Client;
    sessionStorage.setItem('requestedUrl', state.url);
    this.oauthService.events;
    if (
      this.oauthService.hasValidAccessToken() &&
      this.oauthService.hasValidIdToken() && this.accountService.getUserInfo() !== undefined

      // this.oauthService.getIdentityClaims()['UserType'] === "1"
      // && this.userProfile['UserType']==4

    ) {
      if (this.accountService.getUserInfo().UserType !== '4') {
        this.oauthService.revokeTokenAndLogout();
        return false;
      }
      //   console.log(this.oauthService.getIdentityClaims()['UserType']);
      //  console.log(this.oauthService.getIdentityClaims()['UserType'] === "1");
      return true;
    } else {

      // if (client === undefined) {
      //   this.router.navigate(['/autologin']);
      // }
      // else {
        this.router.navigate(['/' + client + '/autologin']);
      // }

        return false;
    }
  }

}
