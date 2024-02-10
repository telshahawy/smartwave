import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as FileSaver from "file-saver";
import {saveAs} from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class FileServiceService {


  constructor(private http: HttpClient) { }
 downloadFile(url:any)
 {
  return  this.http.get(url, {responseType: "blob", headers: {'Accept': 'application/kml'}});
  // application/vnd.google-earth.kml+xml
  
 }
  downloadFile2(url:any)
  {
    FileSaver.saveAs(url, 'Kml File');
  }
  
}
