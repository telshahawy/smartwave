import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  CountryLookup,
  GovernateLookup,
  TimeZoneFramesDto,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-area-create',
  templateUrl: './area-create.component.html',
  styleUrls: ['./area-create.component.css'],
})
export class AreaCreateComponent extends BaseComponent implements OnInit {
  countries: CountryLookup[];
  governats: GovernateLookup[];
  estimatedVisitDurationInMin;
  selectedCountryId: string = null;
  governateId: string = null;
  startTime: Date;
  endTime: Date;
  isActive: boolean = false;
  branchDispatch: boolean = false;
  @ViewChild('createAreaForm') createAreaForm: NgForm;
  baseUrl: string = environment.baseUrl;
  url: any;
  fileName: string;
  fileExtension: any;
  fileExtensionError: boolean;
  fileExtensionMessage: string;
  isView: boolean = true;
  timesArray: TimeZoneFramesDto[] = [];
  noVisitsPattern = '^[1-9][0-9]*(.[0-9]+)?|0+.[0-9]*[1-9][0-9]*$';
  timesArrayObj = new TimeZoneFramesDto();
  submitted: boolean = false;
  visitNoLimit

  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public router: Router,
    public datepipe: DatePipe,
    public notify: NotifyService
  ) {
    super(PagesEnum.Areas, ActionsEnum.Create, router, notify);
    environment;
  }

  ngOnInit(): void {
    this.getCountries();

    this.timesArrayObj = new TimeZoneFramesDto();
    this.timesArray.push(this.timesArrayObj);
    this.getSystemParams()
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  getSystemParams() {
    return this.service.systemParameters.getById().subscribe((items) => {
      this.estimatedVisitDurationInMin = items.response.systemParameters.estimatedVisitDurationInMin;

    });
  }
  onSubmit(form: NgForm) {
    this.submitted = true;
    const dto = form.value;
    if (dto) {
      if (this.timesArray.length == 0) {
        this.notify.error(
          'You must enter at least one visit time',
          'FAILED OPERATION'
        );
        this.submitted = false;
      } else if (this.validationTime()) {
        this.notify.error(
          'Check time list there is start time greater than end time',
          'FAILED OPERATION'
        );
        this.submitted = false;
      } else {
        this.filterTimeFrames();
        dto.timeZoneFrames = this.timesArray;
        if (this.url && this.url.length != 0) {
          dto.kmlFilePath = this.url;
          dto.kmlFileName = this.url.slice(17, 1000);
        }
        this.service.createArea(dto).subscribe(
          (res) => {

            this.router.navigate(['client/area-list']);
            this.notify.saved();
            this.submitted = false;
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');
            this.submitted = false;
          }
        );
      }
    }
  }
  getGovernts(id?: string) {
    return this.service.getGovernats(id).subscribe((items) => {
      this.governats = items.response.governats;
    });
  }

  onOptionsSelected(value: string) {
    this.getGovernts(value);
  }

  getFiles(event) {
    if (event.target.files && event.target.files[0]) {
      var file2 = event.target.files[0];
      this.fileName = file2.name;

      var allowedExtensions = ['kml'];
      this.fileExtension = this.fileName.split('.').pop();

      if (this.isInArray(allowedExtensions, this.fileExtension)) {
        this.fileExtensionError = false;
        this.fileExtensionMessage = '';
        const formData = new FormData();
        let file = event.target.files[0];
        formData.append('personalPhoto', file);
        this.service.uplodaKlmFile(formData).subscribe((res) => {
          this.url = res.response;
          // console.log('index of : ', this.url);
        });
      } else {
        this.fileExtensionMessage = 'Only Kml allowed!!';
        this.fileExtensionError = true;
      }
    }
  }
  isInArray(array, word) {
    return array.indexOf(word.toLowerCase()) > -1;
  }
  backToList() {
    this.router.navigate(['client/area-list']);
  }

  onlyNumberKey(event) {
    return event.charCode == 8 || event.charCode == 0
      ? null
      : event.charCode >= 46 && event.charCode <= 57 
  }
  addItem() {
    this.timesArrayObj = new TimeZoneFramesDto();
    this.branchDispatch = false;
    // if(this.timesArray.length < 2){
      this.timesArray.push(this.timesArrayObj);
      // this.validationTime()
      this.multipleDateRangeOverlaps(this.timesArray)
    // }
    // for (var i = 0; i < this.timesArray.length; i++) {
    //   if(i >1){

    //     console.log(this.timesArray.length);
        
    //   }

    // }

   

  }

   dateRangeOverlaps(a_start, a_end, b_start, b_end) {
     console.log(a_start, a_end, b_start, b_end);
     
    if (a_start.startTime <= b_start.startTime && b_start.startTime <= a_end.endTime) return true; // b starts in a
    if (a_start.startTime <= b_end.endTime   && b_end.endTime   <= a_end.endTime) return true; // b ends in a
    if (b_start.startTime <  a_start.startTime && a_end.endTime   <  b_end.endTime) return true; // a in b
    this.notify.error(
      'Please check time zone list there is overlapping between times',
      'FAILED OPERATION'
    );

    // return false;
}
 multipleDateRangeOverlaps(times) {
    var i, j;
    if (times.length % 2 !== 0){}
        console.log('times length must be a multiple of 2');
    for (i = 0; i < times.length - 2; i += 2) {
        for (j = i + 2; j < times.length; j += 2) {
            if (
                this.dateRangeOverlaps(
                    times[i], times[i+1],
                    times[j], times[j+1]
                )
            ) return true;
        }
    }
    return false;
}
  removeItem(index) {
    this.timesArray.splice(index);
  }
  filterTimeFrames() {
    for (var i = 0; i < this.timesArray.length; i++) {
      if (this.timesArray[i].branchDispatch) {
        this.timesArray[i].visitsNoQuota = 0;
      }
    }
  }
  timeToMins(time) {
    var b = time.split(':');
    return b[0] * 60 + +b[1];
  }

  // Convert minutes to a time in format hh:mm
  // Returned value is in range 00  to 24 hrs
  timeFromMins(mins) {
    function z(n) { return (n < 10 ? '0' : '') + n; }
    var h = (mins / 60 | 0) % 24;
    var m = mins % 60;
    return z(h) + ':' + z(m);
  }
  // Add two times in hh:mm format
  minsTimes(end, start) {
    return this.timeFromMins(this.timeToMins(end) - this.timeToMins(start));
  }
  validateVisitTimeNo(startTime, endTime, index) {

    if (startTime && endTime) {

      const calcTimeWithMin = this.minsTimes(endTime, startTime)
      const total = calcTimeWithMin.split(':'); // split it at the colons

      this.visitNoLimit = ((+total[0]) * 60 + (+total[1])) / this.estimatedVisitDurationInMin;
      this.timesArray[index].maxNumVisitNo = this.visitNoLimit



    }

  }
  validationTime() {
    for (var i = 0; i < this.timesArray.length; i++) {
      let startDate = new Date(
        '1990-01-01 ' + this.timesArray[i].startTime
      ).getTime();
      let endDate = new Date(
        '1990-01-01 ' + this.timesArray[i].endTime
      ).getTime();

      if (startDate > endDate) {
        return true;
      }
    }
    return false;
  }
}
