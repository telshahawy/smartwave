import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private oauthService: OAuthService, private router: Router) {}

  ngOnInit(): void {}
  goTo() {
    this.router.navigate(['alfa/autologin']);
  }
}
