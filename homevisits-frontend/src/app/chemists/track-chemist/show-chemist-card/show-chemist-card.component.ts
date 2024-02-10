import { DatePipe } from '@angular/common';
import { Component, HostListener, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChemistTrackingLog, DialogData } from 'src/app/core/models/models';
import { ShowChemistScheduleComponent } from '../show-chemist-schedule/show-chemist-schedule.component';

@Component({
  selector: 'app-show-chemist-card',
  templateUrl: './show-chemist-card.component.html',
  styleUrls: ['./show-chemist-card.component.css']
})
export class ShowChemistCardComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: ChemistTrackingLog,
    public datepipe: DatePipe,
    private dialogRef: MatDialogRef<ShowChemistCardComponent>,
    public scheduleDialog: MatDialog) { }

  ngOnInit(): void {
  }
  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialogRef.close();
  }
  getVisitInfo(visitNo: string, visitDate: string, visitTime: any) {
    if (visitTime == null)
      return visitNo;
    else {
      var date = this.datepipe.transform(visitDate, "dd/MM/yyyy");
      var time = this.timeFunction(visitTime.value);
      return "#" + visitNo + " - " + date + " " + time
    }
  }
  timeFunction(timeObj) {
    var min = timeObj.minutes < 10 ? "0" + timeObj.minutes : timeObj.minutes;
    var hour = timeObj.hours < 10 ? "0" + timeObj.hours : timeObj.hours;
    return hour + ':' + min;
  };
  getInitails(str: string) {
    var names = str.split(' '),
      initials = names[0].substring(0, 1).toUpperCase();

    if (names.length > 1) {
      initials += names[names.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
  }
  showSchedule() {
    this.dialogRef.close();

    this.scheduleDialog.open(ShowChemistScheduleComponent, {
      width: "800px",
      closeOnNavigation: true,
      data: this.data
    })
  }
}
