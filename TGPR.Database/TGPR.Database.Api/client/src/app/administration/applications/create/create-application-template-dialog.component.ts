import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {ApplicationTypeModel} from '../../../_models/applications/application-type.model';

export interface CreateApplicationTemplateModel {
  Name: string;
  ApplicationTypeId: number;
}

@Component({
  selector: 'tgpr-create-application-template-dialog',
  templateUrl: './create-application-template-dialog.component.html'
})
export class CreateApplicationTemplateDialogComponent {

  public template: CreateApplicationTemplateModel = {
    Name: '',
    ApplicationTypeId: 1
  };

  public applicationTypes: ApplicationTypeModel[];

  constructor(public dialogRef: MatDialogRef<CreateApplicationTemplateDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: ApplicationTypeModel[]) {
    this.applicationTypes = data;
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
