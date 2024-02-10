import { Component, OnInit } from '@angular/core';

@Component({
  selector: '[app-nav-top]',
  templateUrl: './nav-top.component.html',
  styleUrls: ['./nav-top.component.css']
})
export class NavTopComponent implements OnInit {
  isChangePass:boolean=false;
  isChangeUser:boolean=false;
  isChangeUserPass:boolean=false;
  showPassword:boolean=false;
  constructor() { }

  ngOnInit(): void {
  }
  openChangePassword(){
    this.isChangePass = !this.isChangePass
  }
  openChangeUser(){
    this.isChangeUser = !this.isChangeUser
  }
  openShowUserPass(){
    this.isChangeUserPass = !this.isChangeUserPass
  }
  togglePass(){
    this.showPassword= !this.showPassword
  }

}
