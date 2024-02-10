import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { NavigationEnd, Router, Event } from '@angular/router';
// import { OAuthService } from 'angular-oauth2-oidc';
import { AccountService } from 'src/app/core/data-services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnChanges {
  @Input() userName: string;
  @Input() isAuthenticated = false;
  currentPage = '';
  constructor(
    private accountService: AccountService,
    private router: Router // private oauthService: OAuthService
  ) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        // console.log('router : ', this.router.url);
        this.currentPage = this.router.url.slice(
          this.router.url.lastIndexOf('/') + 1,
          1000
        );
        // console.log('currentPage : ', this.currentPage);
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {}

  ngOnInit(): void {
    // if (
    //   this.oauthService.hasValidAccessToken() &&
    //   this.oauthService.hasValidIdToken()
    // ) {
    // this.userName = this.accountService.getProfile();
    // }
  }
  logout() {
    // this.oauthService.revokeTokenAndLogout();
    if (localStorage.getItem('accountType') == 'alfa') {
      this.router.navigate(['users/logout/alfa']);
    } else {
      this.router.navigate(['users/logout/admin']);
    }
  }
}
