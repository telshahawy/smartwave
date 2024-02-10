import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-client-dashboard',
  templateUrl: './client-dashboard.component.html',
  styleUrls: ['./client-dashboard.component.css'],
})
export class ClientDashboardComponent extends BaseComponent implements OnInit {
  constructor(
    public router: Router,
    public notify: NotifyService,
    private service: ClientService
  ) {
    super(PagesEnum.Dashboard, ActionsEnum.View, router, notify);
    if (
      window.location.href.includes('alfa') ||
      localStorage.getItem('accountType') == 'alfa'
    ) {
      localStorage.setItem('accountType', 'alfa');
      if (window.location.href.includes('alfa')) {
        this.router.navigate(['/alfa/client']);
        // this.router.navigate(['/system-configuration']);
      }
    } else {
      localStorage.setItem('accountType', 'admin');
    }
  }
  isAreaSelectOpen: boolean = false;
  query;
  searchText;
  homeStatis;
  Areas = [];
  geozoneId;
  activeChemist = [];
  idleChemist = [];
  showChemist = '';
  loading: boolean;
  AreaName: string = "Area";
  ngOnInit(): void {
    this.loading = true;
    this.getHomeStat('');
    this.getAreas();
    this.getChemistActive();
    this.getIdleActive();
  }
  getHomeStat(geozoneId?: string) {
    this.service.homeService.getHomeStat(geozoneId).subscribe((res) => {
      this.loading = false;
      this.homeStatis = res.response;
      this.isAreaSelectOpen = false;
    });
  }

  getAreas() {
    this.service.homeService.getAreaByUser().subscribe((res) => {
      this.Areas = res.response.userAreas;
    });
  }
  openChemistActive() {
    var myModal = document.getElementById('DuplicateModal');
    myModal.classList.add('show');
    this.searchText = '';
  }
  openIdleActive() {
    var myModal = document.getElementById('DuplicateModal1');
    myModal.classList.add('show');
    this.query = '';
  }
  getChemistActive() {
    this.service.homeService.getActiveChemist().subscribe((res) => {
      res.response.activeChemistNames.forEach((element, i) => {
        this.activeChemist.push({ name: element });
        console.log(element);
        console.log(this.activeChemist);
      });
    });
  }

  getIdleActive() {
    this.service.homeService.getIdleChemists().subscribe((res) => {
      res.response.idleChemistNames.forEach((element, i) => {
        this.idleChemist.push({ name: element });
        console.log(element);
        console.log(this.idleChemist);
      });
    });
  }
  changeDDName(area){
    this.AreaName = area.geoZoneNameEn;
  }
  gotorole() {
    this.router.navigate(['client/role-create']);
  }
}
