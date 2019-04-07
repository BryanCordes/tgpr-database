import {Injectable, Input} from '@angular/core';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';
import {ApplicationOptionService} from '../../../_services/administration/applications/application-option.service';
import {ApplicationOptionModel} from '../../../_models/applications/application-option.model';
import {MatDialog} from '@angular/material';
import {CreateApplicationOptionDialogComponent} from './create/create-application-option-dialog.component';
import {EditApplicationOptionDialogComponent, EditApplicationOptionModel} from './edit/edit-application-option-dialog.component';

@Injectable()
export class ApplicationAdministrationOptionsViewModel {
  public category: ApplicationCategoryModel;
  public question: ApplicationQuestionModel;
  public questionTypes: ApplicationQuestionTypeModel[];

  constructor(private _service: ApplicationOptionService, private _dialog: MatDialog) { }

  public create() {
    const dialogRef = this._dialog.open(CreateApplicationOptionDialogComponent, {
      width: "500px",
      data: this.question
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: ApplicationOptionModel = result;
        if(!modelData){
          return;
        }

        this._service.create(modelData)
          .subscribe(option => {
            this.question.Options.push(option);
          });
      });
  }

  public edit(option: ApplicationOptionModel) {
    let editModel: EditApplicationOptionModel = {
      question: this.question,

      ApplicationOptionId: option.ApplicationOptionId,
      ApplicationQuestionId: option.ApplicationQuestionId,
      ApplicationOptionStatusId: option.ApplicationOptionStatusId,
      Text: option.Text,
      ReviewerSortOrder: option.ReviewerSortOrder
    };

    const dialogRef = this._dialog.open(EditApplicationOptionDialogComponent, {
      width: "400px",
      data: editModel
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: EditApplicationOptionModel = result;
        if(!modelData){
          return;
        }

        let sortOrderChanged: boolean = option.ReviewerSortOrder != modelData.ReviewerSortOrder;

        return this._service.update(modelData)
          .subscribe(savedOption => {
            if(sortOrderChanged) {
              this.resortReviewer(option.ReviewerSortOrder, savedOption.ReviewerSortOrder);

              option.ReviewerSortOrder = savedOption.ReviewerSortOrder;
            }

            option.Text = savedOption.Text;
            option.ApplicationOptionStatusId = savedOption.ApplicationOptionStatusId;
          });
      });
  }

  public moveUp(option: ApplicationOptionModel) {
    option.ApplicationSortOrder = option.ApplicationSortOrder - 1;

    this.updateSortOrder(option, 'up');
  }

  public moveDown(option: ApplicationOptionModel) {
    option.ApplicationSortOrder = option.ApplicationSortOrder + 1;

    this.updateSortOrder(option, 'down');
  }

  public delete(option: ApplicationOptionModel) {
    this._service.delete(option)
      .subscribe(() => {
        let index = this.question.Options
          .findIndex(x => x.ApplicationOptionId == option.ApplicationOptionId);

        this.question.Options.splice(index, 1);

        let length: number = this.question.Options.length;

        this.resortReviewerAbove(this.question.ReviewerSortOrder, length + 1);
        this.resortApplicationAbove(this.question.ApplicationSortOrder);
      });
  }

  private updateSortOrder(option: ApplicationOptionModel, direction: string) {
    this._service.updateSortOrder(option)
      .subscribe(() => {
        this.resort(option, direction);
      });
  }

  private resortReviewer(index: number, newIndex: number) {
    this.resortReviewerBelow(index, newIndex);
    this.resortReviewerAbove(index, newIndex);
  }

  private resortReviewerBelow(index: number, newIndex: number) {
    let options: ApplicationOptionModel[] = this.question.Options
      .filter(x => x.ReviewerSortOrder >= newIndex && x.ReviewerSortOrder < index);
    for(let option of options) {
      option.ReviewerSortOrder++;
    }
  }

  private resortReviewerAbove(index: number, newIndex: number) {
    let options: ApplicationOptionModel[] = this.question.Options
      .filter(x => x.ReviewerSortOrder <= newIndex && x.ReviewerSortOrder > index);
    for(let option of options) {
      option.ReviewerSortOrder--;
    }
  }

  private resort(option: ApplicationOptionModel, direction: string): void {
    let index = this.question.Options
      .findIndex(x => x.ApplicationOptionId == option.ApplicationOptionId);

    let num: number = direction == "up"
      ? -1
      : 1;

    let newIndex: number = index + num;

    let arr = this.question.Options;
    [arr[index], arr[newIndex]] = [arr[newIndex], arr[index]];

    arr[index].ApplicationSortOrder = index - num;
  }

  private resortApplicationAbove(index: number) {
    let options: ApplicationOptionModel[] = this.question.Options
      .filter(x => x.ApplicationSortOrder > index);
    for(let option of options) {
      option.ApplicationSortOrder--;
    }
  }
}
