import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from '../../../auth-code-flow.config';
import {LocalStorageService, SessionStorageService} from 'ngx-webstorage';
@Component({
  selector: 'app-auto-login',
  templateUrl: './auto-login.component.html',
  styleUrls: ['./auto-login.component.css']
})
export class AutoLoginComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private oauthService: OAuthService,
  ) {

    const client = route.snapshot.paramMap.get('Client');
    console.log(client);
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocument().then(_ => {
      // if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
        sessionStorage.setItem('flow', 'code');
        this.oauthService.customQueryParams = {
          client: client,
      };

        this.oauthService.initLoginFlow('/some-state;nonce=123;p1=1;p2=2?p3=3&p4=4');
      // }
      // else{
      //   // this.oauthService.revokeTokenAndLogout();
      // }
    });
  }

  ngOnInit() {
  }

}
