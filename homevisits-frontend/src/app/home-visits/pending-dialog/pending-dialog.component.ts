import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-pending-dialog',
  templateUrl: './pending-dialog.component.html',
  styleUrls: ['./pending-dialog.component.css']
})
export class PendingDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

}
