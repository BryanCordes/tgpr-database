import {Component, Inject} from '@angular/core';
import {ApplicationCategoryModel} from '../../../../_models/applications/application-category.model';
import {ApplicationTemplateModel} from '../../../../_models/administration/applications/application-template.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {ApplicationOptionModel} from '../../../../_models/applications/application-option.model';
import {OptionStatusEnum} from '../../../../_models/administration/applications/option-status.enum';
import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';

@Component({
  selector: 'tgpr-create-application-option-dialog',
  templateUrl: './create-application-option-dialog.component.html'
})
export class CreateApplicationOptionDialogComponent {
  option: ApplicationOptionModel;
  question: ApplicationQuestionModel;

  optionStatuses: any[] = [
    {key: OptionStatusEnum.None, value: "None"},
    {key: OptionStatusEnum.MarkForReview, value: "Mark for Review"},
    {key: OptionStatusEnum.PromptForExit, value: "Prompt for Exit"}
  ];

  optionCount: number[] = [];

  constructor(public dialogRef: MatDialogRef<CreateApplicationOptionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: ApplicationQuestionModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {

    this.question = this.data;

    this.option = {
      ApplicationOptionId: 0,
      ApplicationQuestionId: this.question.ApplicationQuestionId,
      ApplicationOptionStatusId: OptionStatusEnum.None,
      Text: "",
      ApplicationSortOrder: this.question.Options
        ? this.question.Options.length + 1
        : 1,
      ReviewerSortOrder: this.question.Options
        ? this.question.Options.length + 1
        : 1,
      ChildQuestions: []
    };

    for(let i: number = 1; i <= this.question.Options.length + 1; i++) {
      this.optionCount.push(i);
    }
  }
}
