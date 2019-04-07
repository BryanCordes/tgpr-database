import {Component, Inject} from '@angular/core';
import {ApplicationCategoryModel} from '../../../../_models/applications/application-category.model';
import {ApplicationTemplateModel} from '../../../../_models/administration/applications/application-template.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {ApplicationOptionModel} from '../../../../_models/applications/application-option.model';
import {OptionStatusEnum} from '../../../../_models/administration/applications/option-status.enum';
import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';

export interface EditApplicationOptionModel {
  question: ApplicationQuestionModel;

  ApplicationOptionId: number;
  ApplicationQuestionId: number;
  ApplicationOptionStatusId: number;
  Text: string;
  ReviewerSortOrder: number;
}

@Component({
  selector: 'tgpr-edit-application-option-dialog',
  templateUrl: './edit-application-option-dialog.component.html'
})
export class EditApplicationOptionDialogComponent {

  question: ApplicationQuestionModel;
  option: EditApplicationOptionModel;

  optionCount: number[] = [];

  optionStatuses: any[] = [
    {key: OptionStatusEnum.None, value: "None"},
    {key: OptionStatusEnum.MarkForReview, value: "Mark for Review"},
    {key: OptionStatusEnum.PromptForExit, value: "Prompt for Exit"}
  ];

  constructor(public dialogRef: MatDialogRef<EditApplicationOptionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: EditApplicationOptionModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {

    this.question = this.data.question;
    this.option = this.data;

    for(let i: number = 1; i <= this.question.Options.length; i++) {
      this.optionCount.push(i);
    }
  }
}
