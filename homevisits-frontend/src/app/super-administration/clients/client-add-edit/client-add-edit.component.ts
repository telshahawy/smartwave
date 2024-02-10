import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-client-add-edit',
  templateUrl: './client-add-edit.component.html',
  styleUrls: ['./client-add-edit.component.css']
})
export class ClientAddEditComponent implements OnInit {
  showPassword:boolean=false;

  constructor() { }

  ngOnInit(): void {
  }
 togglePass(){
    this.showPassword= !this.showPassword
  }
}
