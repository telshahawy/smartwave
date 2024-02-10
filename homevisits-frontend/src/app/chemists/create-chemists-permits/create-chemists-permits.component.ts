import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { createElementParams } from '@syncfusion/ej2-inputs';
import { Timestamp } from 'rxjs/internal/operators/timestamp';
import { timestamp } from 'rxjs/operators';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { CreateChemistPermitModel } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-create-chemists-permits',
  templateUrl: './create-chemists-permits.component.html',
  styleUrls: ['./create-chemists-permits.component.css']
})
export class CreateChemistsPermitsComponent extends BaseComponent implements OnInit {
  createPermits: CreateChemistPermitModel;
  chemistId: string;
  permitDate: Date;
  startTime: any;
  endTime: any;
  submitted: boolean = false;
  @ViewChild('createForm') createForm: NgForm;
  constructor(
    public router: Router,
    public notify: NotifyService,
    private client: ClientService,
    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {
    super(PagesEnum.ChemistPermit, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {
    this.chemistId = this.route.snapshot.paramMap.get('chemistId');
    this.createPermits= new CreateChemistPermitModel();
    this.createPermits.chemistId = this.chemistId;
  }
  backToList(){
    this.router.navigate([`/chemists/chemists-permits/${this.chemistId}`]);
  }
  onSubmit(form: NgForm) {
    
    this.submitted=true
    
    const dto = form.value;
    if (dto) {
      this.createPermits.permitDate= dto.permitDate;
      this.createPermits.startTime = dto.startTime;
      this.createPermits.endTime = dto.endTime;
      this.client.createChemistPermits(this.createPermits).subscribe(
        (res) => {
          form.reset({
            permitDate:'',
            startTime:'',
            endTime:'' 
           });
         
           this.router.navigate([`/chemists/chemists-permits/${this.chemistId}`]);
          this.notify.saved();
    this.submitted=false

         
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
    this.submitted=false

        }
      );
    }
  }
}
