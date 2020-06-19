import {Injectable, OnInit} from '@angular/core';
import {ApplicationQuestionService} from '../../../_services/administration/applications/application-question.service';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {
  CreateApplicationQuestionModel,
  CreateApplicationQuestionQuestionDialogComponent
} from './create/create-application-question-dialog.component';
import {MatDialog} from '@angular/material';
import {ApplicationOptionModel} from '../../../_models/applications/application-option.model';
import {EditApplicationQuestionDialogComponent, EditApplicationQuestionModel} from './edit/edit-application-question-dialog.component';
import {QuestionTypeEnum} from '../../../_models/administration/applications/question-type.enum';

@Injectable()
export class ApplicationAdministrationQuestionsViewModel {

  public category: ApplicationCategoryModel;
  public questionTypes: ApplicationQuestionTypeModel[];
  public option: ApplicationOptionModel;

  public questions: ApplicationQuestionModel[] = [];

  constructor(private _service: ApplicationQuestionService, private _dialog: MatDialog) { }

  public create() {
    let data: CreateApplicationQuestionModel = {
      category: this.category,
      questionTypes: this.questionTypes,
      option: this.option
    };

    const dialogRef = this._dialog.open(CreateApplicationQuestionQuestionDialogComponent, {
      width: "500px",
      data: data
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: ApplicationQuestionModel = result;
        if(!modelData){
          return;
        }

        if(this.option){
          modelData.ParentApplicationOptionId = this.option.ApplicationOptionId;
        }

        this._service.create(modelData)
          .subscribe(question => {
            this.questions.push(question);
          });
      });
  }

  public moveUp(question: ApplicationQuestionModel) {
    question.ApplicationSortOrder = question.ApplicationSortOrder - 1;

    this.updateSortOrder(question, 'up');
  }

  public moveDown(question: ApplicationQuestionModel) {
    question.ApplicationSortOrder = question.ApplicationSortOrder + 1;

    this.updateSortOrder(question, 'down');
  }

  public edit(question: ApplicationQuestionModel) {
    let questionCount: number = this.questions.length;

    let editModel: EditApplicationQuestionModel ={
      questionTypes: this.questionTypes,
      questionCount: questionCount,
      ApplicationQuestionId: question.ApplicationQuestionId,
      ApplicationQuestionTypeId: question.ApplicationQuestionTypeId,
      Text: question.Text,
      ReviewerSortOrder: question.ReviewerSortOrder,
      Width: question.Width
    };

    const dialogRef = this._dialog.open(EditApplicationQuestionDialogComponent, {
      width: "400px",
      data: editModel
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: EditApplicationQuestionModel = result;
        if(!modelData){
          return;
        }

        let sortOrderChanged: boolean = question.ReviewerSortOrder != editModel.ReviewerSortOrder;

        this._service.update(modelData)
          .subscribe(savedQuestion => {
            if(sortOrderChanged) {
              this.resortReviewer(question.ReviewerSortOrder, savedQuestion.ReviewerSortOrder);

              question.ReviewerSortOrder = savedQuestion.ReviewerSortOrder;
            }

            question.Text = savedQuestion.Text;
            question.ApplicationQuestionTypeId = savedQuestion.ApplicationQuestionTypeId;
            question.Width = savedQuestion.Width;
            question.Options = savedQuestion.Options;
          });
      });
  }

  public delete(question: ApplicationQuestionModel) {
    this._service.delete(question)
      .subscribe(() => {
        let index = this.questions
          .findIndex(x => x.ApplicationQuestionId == question.ApplicationQuestionId);

        this.questions.splice(index, 1);

        let length: number = this.questions.length;

        this.resortReviewerAbove(question.ReviewerSortOrder, length + 1);
        this.resortApplicationAbove(question.ApplicationSortOrder);
      });
  }

  public getDescription(question: ApplicationQuestionModel) {
    if(!this.questionTypes || this.questionTypes.length == 0) {
      return "";
    }

    let type = this.questionTypes.find(x => x.ApplicationQuestionTypeId == question.ApplicationQuestionTypeId);
    if(!type) {
      return "";
    }

    let description: string = `Type: ${type.Name} | Review Order: ${question.ReviewerSortOrder} | Width: ${question.Width}`;

    return description;
  }

  public hasEditableOptions(question: ApplicationQuestionModel): boolean {
    if(!question) {
      return false;
    }

    let optionQuestionTypes: number[] = [
      QuestionTypeEnum.SingleSelection,
      QuestionTypeEnum.MultipleSelection,
      QuestionTypeEnum.Dropdown
    ];

    let type: number = optionQuestionTypes.find(x => x == question.ApplicationQuestionTypeId);

    return !!type;
  }

  public load() {
    this.questions = this.option
      ? this.option.ChildQuestions
      : this.category.Questions;
  }

  private updateSortOrder(question: ApplicationQuestionModel, direction: string) {
    this._service.updateSortOrder(question)
      .subscribe(() => {
        this.resort(question, direction);
      });
  }

  private resort(question: ApplicationQuestionModel, direction: string): void {
    let index = this.questions
      .findIndex(x => x.ApplicationQuestionId == question.ApplicationQuestionId);

    let num: number = direction == "up"
      ? -1
      : 1;

    let newIndex: number = index + num;

    let arr = this.questions;
    [arr[index], arr[newIndex]] = [arr[newIndex], arr[index]];

    arr[index].ApplicationSortOrder = index - num;
  }

  private resortReviewer(index: number, newIndex: number) {
    this.resortReviewerBelow(index, newIndex);
    this.resortReviewerAbove(index, newIndex);
  }

  private resortReviewerBelow(index: number, newIndex: number) {
    let questions: ApplicationQuestionModel[] = this.questions
      .filter(x => x.ReviewerSortOrder >= newIndex && x.ReviewerSortOrder < index);
    for(let question of questions) {
      question.ReviewerSortOrder++;
    }
  }

  private resortReviewerAbove(index: number, newIndex: number) {
    let questions: ApplicationQuestionModel[] = this.questions
      .filter(x => x.ReviewerSortOrder <= newIndex && x.ReviewerSortOrder > index);
    for(let question of questions) {
      question.ReviewerSortOrder--;
    }
  }

  private resortApplicationAbove(index: number) {
    let questions: ApplicationQuestionModel[] = this.questions
      .filter(x => x.ApplicationSortOrder > index);
    for(let question of questions) {
      question.ApplicationSortOrder--;
    }
  }
}
