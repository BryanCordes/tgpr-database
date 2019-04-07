import {Component, Inject} from '@angular/core';
import {ApplicationTemplateModel} from '../../../../_models/administration/applications/application-template.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

export interface EditApplicationCategoryModel {
  template: ApplicationTemplateModel;
  ApplicationCategoryId: number;
  Name: string;
  ReviewerSortOrder: number;
  HasReview: boolean;
}

@Component({
  selector: 'tgpr-edit-application-category-dialog',
  templateUrl: './edit-application-category-dialog.component.html'
})
export class EditApplicationCategoryDialogComponent {

  category: EditApplicationCategoryModel;
  categoryCount: number[] = [];

  constructor(public dialogRef: MatDialogRef<EditApplicationCategoryDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: EditApplicationCategoryModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    let template = this.data.template;

    this.category = this.data;

    for(let i: number = 1; i <= template.Categories.length; i++) {
      this.categoryCount.push(i);
    }
  }
}
