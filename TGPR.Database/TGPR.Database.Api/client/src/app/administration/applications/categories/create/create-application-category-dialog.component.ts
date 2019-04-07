import {Component, Inject} from '@angular/core';
import {ApplicationCategoryModel} from '../../../../_models/applications/application-category.model';
import {ApplicationTemplateModel} from '../../../../_models/administration/applications/application-template.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'tgpr-create-application-category-dialog',
  templateUrl: './create-application-category-dialog.component.html'
})
export class CreateApplicationCategoryDialogComponent {
  category: ApplicationCategoryModel;
  template: ApplicationTemplateModel;

  categoryCount: number[] = [];

  constructor(public dialogRef: MatDialogRef<CreateApplicationCategoryDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: ApplicationTemplateModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.template = this.data;

    this.category = {
      ApplicationCategoryId: 0,
      ApplicationTemplateId: this.data.ApplicationTemplateId,
      Name: "",
      ApplicationSortOrder: this.data.Categories
        ? this.data.Categories.length + 1
        : 0,
      ReviewerSortOrder: this.data.Categories
        ? this.data.Categories.length + 1
        : 0,
      Deleted: false,
      HasReview: false,
      Questions: [],
      Review: null
    };

    for(let i: number = 1; i <= this.template.Categories.length + 1; i++) {
      this.categoryCount.push(i);
    }
  }
}
