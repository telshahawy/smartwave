import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { ReasonsList, ReasonsObj } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-cancel-visit',
  templateUrl: './cancel-visit.component.html',
  styleUrls: ['./cancel-visit.component.css']
})
export class CancelVisitComponent extends BaseComponent implements OnInit {
  @ViewChild('cancelVisitForm') cancelVisitForm: NgForm;
  reasons:any[]=[];
  ReasonObj:ReasonsObj;
  reasonId:number=null;
  visitId:any;
  status:any;
  ActionType:number;
  constructor(private service:ClientService,public dialog: MatDialog,@Inject(MAT_DIALOG_DATA) data,public notify:NotifyService,public router: Router) { 
    super(PagesEnum.HomeVisit,ActionsEnum.Cancel,router,notify)
    this.visitId=data.visitId;
    this.status=data.status;
    if(this.status=="New")
    {
     this.ActionType=8;
    }
    else
    {
      this.ActionType=4;
    }
  }

  ngOnInit(): void {
    this.getReasons();
  }


  getReasons() {
    
    //let ActionType=4;
    return this.service.searchReasonByAction(this.ActionType).subscribe(items => {
      if(items.response.reasons.length>0)
      {
        items.response.reasons.forEach(element => {
          this.ReasonObj=new ReasonsObj();
          this.ReasonObj.reasonId=element.reasonId;
          this.ReasonObj.reasonName=element.reasonName;
          this.reasons.push( this.ReasonObj);
        });
      }
    });
  }
  onSubmit(form: NgForm) {
    console.log('Your form data : ', form.value);
    const dto = form.value;
    if (dto) {
      dto.reasonId=parseInt(dto.reasonId);
      dto.visitId=this.visitId;
      dto.visitActionTypeId=this.ActionType;
      dto.visitStatusTypeId=this.ActionType;
      this.service.sendChemistAction(dto).subscribe(res => {
        console.log(res);
        this.notify.saved();
        this.dialog.closeAll();
      
      }, error => {
        this.notify.error(error.message,'FAILED OPERATION');
        console.log(error);
      });
    
    }
  }
  close()
  {
    this.dialog.closeAll();
  }

}
