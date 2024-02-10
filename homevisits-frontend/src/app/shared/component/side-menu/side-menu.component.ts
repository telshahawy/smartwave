import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { ClientService } from 'src/app/core/data-services/client.service';
import { PermissionsService } from 'src/app/core/data-services/permissions.service';
import { MenuPage } from 'src/app/core/permissions/models/menuPage';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css'],
})
export class SideMenuComponent implements OnInit {
  constructor(
    private router: Router,
    private permissionsService: PermissionsService,
    private client: ClientService,
    private oauthService: OAuthService
  ) {}
  @Input() isAuthenticated = false;

  isOpenNav: string;
  showSide = false;
  menuPages: MenuPage[] = [];

  ngOnInit() {
    if (this.isAuthenticated) {
      let items = JSON.parse(sessionStorage.getItem('menu'));
      this.menuPages = items;
    }
  }
  ngOnChanges() {
    if (this.isAuthenticated) {
      let items = JSON.parse(sessionStorage.getItem('menu'));
      this.menuPages = items;
    }
  }
  gotoVisits() {
    this.router.navigate(['visits/visits-list']);
  }

  openNav(current) {
    this.isOpenNav = current;
    let circleDivElement;
    let elements = document.querySelectorAll('.collapse');
    elements.forEach((e) => {
      if (e.attributes.getNamedItem('Id').value == current)
        circleDivElement = e;
    });
    document.querySelectorAll('.showw').forEach((e) => {
      e.classList.remove('showw');
    });
    if (circleDivElement.classList.contains('show')) {
      circleDivElement.classList.remove('showw');
    } else {
      circleDivElement.classList.add('showw');
    }
  }

  openningNav() {
    setTimeout(() => {
      return 'collapsing';
    }, 100);
    return 'show';
  }
}
