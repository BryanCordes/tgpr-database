import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material';

export interface TimeoutDialogData {
  countdown: number;
}

@Component({
  selector: 'tgpr-timeout-dialog',
  templateUrl: './timeout.dialog.html'
})
export class TimeoutDialogComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: TimeoutDialogData) { }

}
