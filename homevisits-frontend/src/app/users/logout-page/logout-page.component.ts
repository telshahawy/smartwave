import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-logout-page',
  templateUrl: './logout-page.component.html',
  styleUrls: ['./logout-page.component.css'],
})
export class LogoutPageComponent implements OnInit {
  constructor(private router: Router, private oauthService: OAuthService) {
    if (localStorage.getItem('accountType')) {
      localStorage.setItem(
        'accountTypeAfterLogin',
        localStorage.getItem('accountType')
      );
      localStorage.removeItem('accountType');
      this.oauthService.revokeTokenAndLogout();
    } else {
      if (localStorage.getItem('accountTypeAfterLogin') == 'alfa') {
        this.router.navigate(['alfa/autologin']);
      } else {
        this.router.navigate(['autologin']);
      }
    }
  }

  ngOnInit(): void {}
}
