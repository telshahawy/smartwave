import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ClientService } from 'src/app/core/data-services/client.service';
import { ChemistTrackingLog, ChemistVisitsScheduleModel } from 'src/app/core/models/models';
import { ShowChemistCardComponent } from '../show-chemist-card/show-chemist-card.component';

@Component({
  selector: 'app-show-chemist-schedule',
  templateUrl: './show-chemist-schedule.component.html',
  styleUrls: ['./show-chemist-schedule.component.css']
})
export class ShowChemistScheduleComponent implements OnInit {
  isLoaded: boolean = false
  displayedColumns: string[] = [
    'time',
    'area',
    'patientName',
    'address',
    'status',
  ];
  today: Date = new Date()
  schedule: ChemistVisitsScheduleModel[] =[]
  constructor(private service: ClientService,
    @Inject(MAT_DIALOG_DATA) public data: ChemistTrackingLog,
    public datepipe: DatePipe,
    private dialogRef: MatDialogRef<ShowChemistScheduleComponent>) { }

  ngOnInit(): void {
    this.service.GetChemistScheduleById(this.data.chemistId).subscribe(result => {
      this.schedule = result.response;
      this.isLoaded = true;
    });
   
    // this.schedule = [{
    //   areaName: "nasr city",
    //   address: " nasr city nasr city nasr city nasr city ",
    //   endTime: "13:00",
    //   startTime: "13:00",
    //   statusName: "Pending"
    // }, {
    //   areaName: "october city",
    //   address: " october city october city october city october city ",
    //   endTime: "16:00",
    //   startTime: "11:00",
    //   statusName: "Started"
    // }, {
    //   areaName: "Maadi city",
    //   address: " Maadi city Maadi city Maadi city Maadi city ",
    //   endTime: "15:00",
    //   startTime: "15:00",
    //   statusName: "Confirmed"
    // },]
  }
  getInitails(str: string) {
    var names = str.split(' '),
      initials = names[0].substring(0, 1).toUpperCase();

    if (names.length > 1) {
      initials += names[names.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
  }
  timeFunction(timeObj) {
    var min = timeObj.minutes < 10 ? "0" + timeObj.minutes : timeObj.minutes;
    var hour = timeObj.hours < 10 ? "0" + timeObj.hours : timeObj.hours;
    return hour + ':' + min;
  };
  close() {
    this.dialogRef.close();
  }

}
