import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { UpdateChemistPermitModel } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-edit-chemists-permits',
  templateUrl: './edit-chemists-permits.component.html',
  styleUrls: ['./edit-chemists-permits.component.css']
})
export class EditChemistsPermitsComponent extends BaseComponent implements OnInit {
  submitted = false;
  chemistPermistId: string;
  chemistId: string;
  permitDate: Date;
  startTime: any;
  endTime: any;
  editPermits: UpdateChemistPermitModel;
  @ViewChild('editForm') editForm: NgForm;
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public router: Router,
    public notify: NotifyService,
    public datepipe: DatePipe,
  ) {
    super(PagesEnum.ViewChemists, ActionsEnum.Update, router, notify);
  }

  ngOnInit(): void {
    this.editPermits = new UpdateChemistPermitModel();
    this.route.params.subscribe((paramsId) => {
      this.chemistPermistId = paramsId.chemistPermitId;
    });
    if (this.chemistPermistId != undefined) {
      this.getChemistPermit(this.chemistPermistId);
      
    }
  }
  backToList(){
    this.router.navigate([`/chemists/chemists-permits/${this.chemistId}`]);
  }
  public getChemistPermit(chemistPermitId){
    
    this.service.getChemistPermit(chemistPermitId).subscribe((res) => {
      console.log(res.response.permit);
      this.chemistId = res.response.permit.chemistId;
      this.permitDate = res.response.permit.permitDate;
      this.startTime = res.response.permit.startTime;
      this.endTime = res.response.permit.endTime;
      
    });
    //this.startTime = this.timeFunction(this.startTime);
  }
  timeFunction(timeObj) {
    var min = timeObj.minutes < 10 ? "0" + timeObj.minutes : timeObj.minutes;
    var hour = timeObj.hours < 10 ? "0" + timeObj.hours : timeObj.hours;
    return hour + ':' + min;
  };
  onSubmit(form: NgForm){
    
    this.submitted=true
    
    const dto = form.value;
    if (dto) {
      this.editPermits.permitDate= dto.permitDate;
      this.editPermits.startTime = dto.startTime;
      this.editPermits.endTime = dto.endTime;
      this.service.updateChemistPermit(this.editPermits,this.chemistPermistId).subscribe(
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
