import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { time } from 'console';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  CountryLookup,
  GovernateLookup,
  TimeZoneFramesDto,
} from 'src/app/core/models/models';
import { environment } from 'src/environments/environment';
import * as FileSaver from 'file-saver';
import { FileServiceService } from 'src/app/core/data-services/file-service.service';
import { saveAs } from 'file-saver';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-area-edit',
  templateUrl: './area-edit.component.html',
  styleUrls: ['./area-edit.component.css'],
})
export class AreaEditComponent extends BaseComponent implements OnInit {
  countries: CountryLookup[];
  governats: GovernateLookup[];
  selectedCountryId: string;
  selectedGovernateId: string;
  startTime: Date;
  endTime: Date;
  isActive: boolean = false;
  branchDispatch: boolean = false;
  @ViewChild('editAreaForm') editAreaForm: NgForm;
  baseUrl: string = environment.baseUrl;
  url: any;
  url2: any;
  isView: boolean = true;
  timesArray: TimeZoneFramesDto[] = [];
  areaId: string;
  fileName: string;
  fileNamee: string;
  fileExtension: any;
  fileExtensionError: boolean;
  fileExtensionMessage: string;
  showFile: boolean = true;
  submitted: boolean = false;
  noVisitsPattern = '^[1-9][0-9]*(.[0-9]+)?|0+.[0-9]*[1-9][0-9]*$';
  timesArrayObj = new TimeZoneFramesDto();
  estimatedVisitDurationInMin
  visitNoLimit
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public router: Router,
    public datepipe: DatePipe,
    public notify: NotifyService,
    private fileService: FileServiceService
  ) {
    super(PagesEnum.Areas, ActionsEnum.Update, router, notify);

  }

  ngOnInit(): void {
    this.getSystemParams()

    this.route.params.subscribe((paramsId) => {
      this.areaId = paramsId.gezoneId;
    });
    this.getCountries();
    this.getGovernts();
    this.timesArrayObj = new TimeZoneFramesDto();
    this.timesArray.push(this.timesArrayObj);
    if (this.areaId != undefined) {
      this.getArea(this.areaId);
    }
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  onSubmit(form: NgForm) {
    this.submitted = true;
console.log(this.editAreaForm.form);

    if(this.editAreaForm.form.errors){
      this.notify.error(
        '',
        'FAILED OPERATION'
      );      
    this.submitted = false;

      return;
    }

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
          'Please check time zone list there is overlapping between times',
          'FAILED OPERATION'
        );
        this.submitted = false;
      } else {
        this.filterTimeFrames();
        dto.timeZoneFrames = this.timesArray;

        dto.kmlFilePath = this.url;
        dto.kmlFileName = this.fileNamee;
        this.service.updateArea(dto).subscribe(
          (res) => {
            console.log(res);
            this.router.navigate(['client/area-list']);
            this.notify.update();
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
  getArea(id: string) {
    this.service.getArea(id).subscribe((res) => {
      this.editAreaForm.controls['name'].setValue(res.response.geoZoneName);
      this.editAreaForm.controls['code'].setValue(res.response.code);
      this.editAreaForm.controls['geoZoneId'].setValue(res.response.geoZoneId);
      this.editAreaForm.controls['mappingCode'].setValue(
        res.response.mappingCode
      );
      this.editAreaForm.controls['countryId'].setValue(res.response.countryId);
      this.editAreaForm.controls['governateId'].setValue(
        res.response.governateId
      );
      this.editAreaForm.controls['isActive'].setValue(res.response.isActive);
      this.url = res.response.kmlFilePath;
      this.fileNamee = res.response.kmlFileName;
      this.url2 = this.baseUrl + res.response.kmlFilePath;
      if (this.url) {
        this.showFile = false;
      }
      if (res.response.timeZoneFrames.length > 0) {
        this.timesArray = res.response.timeZoneFrames.map((o) => {
          return o;
        });
      }
      for (var i = 0; i < this.timesArray.length; i++) {
        this.timesArray[i].startTime = this.convertTime12to24(
          this.timesArray[i].startTime
        );
        this.timesArray[i].endTime = this.convertTime12to24(
          this.timesArray[i].endTime
        ); //this.stringToDate(this.timesArray[i].startTime);
      }
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
        formData.append('fileKey', file);
        this.service.uplodaKlmFile(formData).subscribe((res) => {
          this.url = res.response;
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
      : event.charCode >= 46 && event.charCode <= 57;
  }
  addItem() {
    this.timesArrayObj = new TimeZoneFramesDto();
    this.branchDispatch = false;
    this.timesArray.push(this.timesArrayObj);
    this.multipleDateRangeOverlaps(this.timesArray)

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

  convertTime12to24 = (time12h) => {
    const [time, modifier] = time12h.split(' ');

    let [hours, minutes] = time.split(':');

    if (hours === '12') {
      hours = '00';
    }

    if (modifier === 'PM') {
      hours = parseInt(hours, 10) + 12;
    }

    return `${hours}:${minutes}`;
  };
  filterTimeFrames() {
    for (var i = 0; i < this.timesArray.length; i++) {
      if (this.timesArray[i].branchDispatch) {
        this.timesArray[i].visitsNoQuota = 0;
      }
    }
  }
  removeFile() {
    this.showFile = true;
    this.url = null;
    this.fileNamee = null;
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
  getSystemParams() {
    return this.service.systemParameters.getById().subscribe((items) => {
      this.estimatedVisitDurationInMin = items.response.systemParameters.estimatedVisitDurationInMin;

    });
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
  download() {
    this.service.downloadKmlFile(this.fileNamee).subscribe((res) => {
      // console.log('res : ', res);
      var blob = new Blob([res]);
      var filename = this.fileNamee + '.kml';
      saveAs(blob, filename);
      // var blob = new Blob([res]);
      // var objectUrl = URL.createObjectURL(blob);
      // window.open(objectUrl);
    });

    // FileSaver.saveAs(this.url2, 'Kml File');
    // this.fileService.downloadFile(this.url2).subscribe(blob => {
    //   console.log(blob);

    //   // saveAs(blob, 'download.kml');
    // });
  }
}
