import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";

@Component({
  selector: 'tgpr-rename-application-template-dialog',
  templateUrl: './rename-application-template-dialog.component.html'
})
export class RenameApplicationTemplateDialogComponent {

  templateName: string;

  constructor(public dialogRef: MatDialogRef<RenameApplicationTemplateDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: string) {
    this.templateName = data;
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
