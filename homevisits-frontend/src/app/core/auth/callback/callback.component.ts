import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from 'src/app/auth-code-flow.config';
import { useHash } from 'src/flags';
import { AccountService } from '../../data-services/account.service';
import { UserPermission } from '../../permissions/models/userPermisions';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.css']
})
export class CallbackComponent implements OnInit {

  constructor(private router: Router, private oauthService: OAuthService, private accountService: AccountService) {
    this.configureCodeFlow();

  }
  ngOnInit(): void {
  }
  private configureCodeFlow() {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
      this.oauthService.loadUserProfile().then((info) => {
        console.log('info');
        console.log(info);
        this.accountService.setUserInfo(info);
        let requestedUrl = sessionStorage.getItem('requestedUrl');

        if (useHash) {
        this.router.navigate(['/']);
        
      }
        this.router.navigate([requestedUrl]);
      });

      // sessionStorage.removeItem("requestedUrl");
    });
  }
}
