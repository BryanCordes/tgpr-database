import {Component, Inject, OnInit} from '@angular/core';
import {ApplicationQuestionTypeModel} from '../../../../_models/applications/application-question-type.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

export interface EditApplicationQuestionModel {
  questionTypes: ApplicationQuestionTypeModel[];
  questionCount: number;

  ApplicationQuestionId: number;
  ApplicationQuestionTypeId: number;
  Text: string;
  ReviewerSortOrder: number;
  Width: number;
}

@Component({
  selector: 'tgpr-edit-application-question-dialog',
  templateUrl: './edit-application-question-dialog.component.html'
})
export class EditApplicationQuestionDialogComponent implements OnInit {

  public editModel: EditApplicationQuestionModel;
  public questionCount: number[];
  questionWidths: number[] = [];


  constructor(public dialogRef: MatDialogRef<EditApplicationQuestionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: EditApplicationQuestionModel) { }

  cancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.editModel = this.data;

    this.questionCount = [];

    for(let i: number = 1; i <= this.editModel.questionCount; i++) {
      this.questionCount.push(i);
    }

    for (let i: number = 1; i <= 12; i++) {
      this.questionWidths.push(i);
    }
  }
}
