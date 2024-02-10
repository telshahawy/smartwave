import {
  OAuthService,
  NullValidationHandler,
  UserInfo,
} from 'angular-oauth2-oidc';
import { filter } from 'rxjs/operators';
import { authCodeFlowConfig } from './auth-code-flow.config';
import { useHash } from '../flags';
import { AccountService } from './core/data-services/account.service';
import { UserPermission } from './core/permissions/models/userPermisions';
import { PagesEnum } from './core/permissions/models/pages';
import { ActionsEnum } from './core/permissions/models/actions';
import { ClientService } from './core/data-services/client.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PermissionsService } from './core/data-services/permissions.service';
// import { browser } from 'protractor';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'homevisits-frontend';
  userProfile: UserInfo;
  isAuthenticated = false;
  userPermissions: UserPermission[]
  constructor(
    private router: Router,
    private oauthService: OAuthService,
    private accountService: AccountService,
    private client : ClientService,
    private permissionsService : PermissionsService
  ) {
    if (
      sessionStorage.getItem('expires_at') &&
      localStorage.getItem('user-token')
    ) {
      var expirationDate = sessionStorage.getItem('expires_at');
      // var currentDate = new Date().getTime() / 1000;
      var difference = parseInt(expirationDate) - new Date().getTime() / 1000;
      // console.log('difference : ', difference * 1000);

      setTimeout(function () {
        location.reload();
      }, difference * 1000);
    }

    // if(
    //   this.oauthService.hasValidAccessToken() &&
    //     this.oauthService.hasValidIdToken()){
    //       this.oauthService.configure(authCodeFlowConfig);
    //        this.oauthService.loadDiscoveryDocument().then(() =>{
    //         this.oauthService.loadUserProfile().then((info) =>{
    //           this.accountService.setUserInfo(info);
    //         });
    //       });

    //     }
    // Remember the selected configuration
    if (sessionStorage.getItem('flow') === 'code') {
      // this.configureCodeFlow();
    } else {
    }

    // Automatically load user profile
    this.oauthService.events
      .pipe(filter((e) => e.type === 'token_received'))
      .subscribe((_) => {
        console.debug('state', this.oauthService.state);
        this.oauthService.loadUserProfile().then((userProfile) => {
          this.accountService.setUserInfo(userProfile);
          this.userProfile = userProfile;
          if (this.oauthService.state !== undefined) {
            // return this.router.navigate([this.oauthService.state[""]]);
          }
          this.accountService.setAccessToken(this.oauthService.getAccessToken());
          // console.log(this.oauthService.state.redirectUrl);
          this.client.GetUserPermissions(userProfile.sub).subscribe((result) => {
             this.permissionsService.MapPermissionsWithMenu(result);
             this.isAuthenticated = true;
           });
        });
      });
  }

  ngOnInit() {}

  private configureCodeFlow() {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then((_) => {
      if (useHash) {
        this.router.navigate(['/']);
      }
    });

    // Optional
    this.oauthService.setupAutomaticSilentRefresh();
  }

  //
  // Below you find further examples for configuration functions
  //
}
