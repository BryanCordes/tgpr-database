import {Component, Inject, OnInit} from '@angular/core';
import {ApplicationOptionModel} from '../../../../_models/applications/application-option.model';
import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';
import {ApplicationQuestionTypeModel} from '../../../../_models/applications/application-question-type.model';
import {ApplicationCategoryModel} from '../../../../_models/applications/application-category.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

export interface CreateApplicationQuestionModel {
  category: ApplicationCategoryModel;
  questionTypes: ApplicationQuestionTypeModel[];
  option?: ApplicationOptionModel;
}

@Component({
  selector: 'tgpr-create-application-question-dialog',
  templateUrl: './create-application-question-dialog.component.html'
})
export class CreateApplicationQuestionQuestionDialogComponent implements OnInit {

  question: ApplicationQuestionModel;
  category: ApplicationCategoryModel;

  questionTypes: ApplicationQuestionTypeModel[];

  questionCount: number[] = [];

  constructor(public dialogRef: MatDialogRef<CreateApplicationQuestionQuestionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: CreateApplicationQuestionModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.questionTypes = this.data.questionTypes;

    this.category = this.data.category;

    let questions: ApplicationQuestionModel[];
    if(this.data.option) {
      questions = this.data.option.ChildQuestions;
    } else {
      questions = this.data.category.Questions;
    }

    this.question = {
      ApplicationQuestionId: 0,
      ApplicationCategoryId: this.category.ApplicationCategoryId,
      ApplicationQuestionTypeId: 1,
      Text: "",
      ApplicationSortOrder: questions
        ? questions.length + 1
        : 1,
      ReviewerSortOrder: questions
        ? questions.length + 1
        : 1,
      Options: [],
      Answer: null
    };

    for(let i: number = 1; i <= questions.length + 1; i++) {
      this.questionCount.push(i);
    }
  }
}
