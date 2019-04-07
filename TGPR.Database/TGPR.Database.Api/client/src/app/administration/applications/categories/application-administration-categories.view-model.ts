import {Injectable} from '@angular/core';
import {ApplicationTemplateModel} from '../../../_models/administration/applications/application-template.model';
import {ApplicationCategoryService} from '../../../_services/administration/applications/application-category.service';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {MatDialog} from '@angular/material';
import {CreateApplicationCategoryDialogComponent} from './create/create-application-category-dialog.component';
import {EditApplicationCategoryDialogComponent, EditApplicationCategoryModel} from './edit/edit-application-category-dialog.component';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';

@Injectable()
export class ApplicationAdministrationCategoriesViewModel {

  public template: ApplicationTemplateModel;
  public questionTypes: ApplicationQuestionTypeModel[];

  constructor(private _service: ApplicationCategoryService, private _dialog: MatDialog) { }

  public create() {
    const dialogRef = this._dialog.open(CreateApplicationCategoryDialogComponent, {
      width: "400px",
      data: this.template
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: ApplicationCategoryModel = result;
        if(!modelData){
          return;
        }

        this._service.create(modelData)
          .subscribe(savedCategory => {
            if(!this.template.Categories){
              this.template.Categories = [];
            }

            this.template.Categories.push(savedCategory);
          });
      });
  }

  public moveUp(category: ApplicationCategoryModel) {
    category.ApplicationSortOrder = category.ApplicationSortOrder - 1;

    this.updateSortOrder(category, 'up');
  }

  public moveDown(category: ApplicationCategoryModel) {
    category.ApplicationSortOrder = category.ApplicationSortOrder + 1;

    this.updateSortOrder(category, 'down');
  }

  public edit(category: ApplicationCategoryModel) {
    const dialogRef = this._dialog.open(EditApplicationCategoryDialogComponent, {
      width: "400px",
      data: {
        template: this.template,
        ApplicationCategoryId: category.ApplicationCategoryId,
        Name: category.Name,
        ReviewerSortOrder: category.ReviewerSortOrder,
        HasReview: category.HasReview
      }
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: EditApplicationCategoryModel = result;
        if(!modelData){
          return;
        }

        let sortOrderChanged: boolean = category.ReviewerSortOrder != modelData.ReviewerSortOrder;

        this._service.update(modelData)
          .subscribe(savedCategory => {
            if(sortOrderChanged) {
              this.resortReviewer(category.ReviewerSortOrder, savedCategory.ReviewerSortOrder);

              category.ReviewerSortOrder = savedCategory.ReviewerSortOrder;
            }

            category.Name = savedCategory.Name;
            category.HasReview = savedCategory.HasReview;
          });
      });
  }

  public delete(category: ApplicationCategoryModel) {
    this._service.delete(category)
      .subscribe(() => {
        let index = this.template.Categories
          .findIndex(x => x.ApplicationCategoryId == category.ApplicationCategoryId);

        this.template.Categories.splice(index, 1);

        let length: number = this.template.Categories.length;

        this.resortReviewerAbove(category.ReviewerSortOrder, length + 1);

        this.resortApplicationAbove(category.ApplicationSortOrder);
      });
  }

  public getDescription(category: ApplicationCategoryModel): string {
    let description: string = `Review Order: ${category.ReviewerSortOrder}`;
    if(category.HasReview) {
      description += ", Has Review";
    }

    return description;
  }

  private updateSortOrder(category: ApplicationCategoryModel, direction: string) {
    this._service.updateSortOrder(category)
      .subscribe(() => {
        this.resort(category, direction);
      });
  }

  private resort(category: ApplicationCategoryModel, direction: string): void {
    let index = this.template.Categories
      .findIndex(x => x.ApplicationCategoryId == category.ApplicationCategoryId);

    let num: number = direction == "up"
      ? -1
      : 1;

    let newIndex: number = index + num;

    let arr = this.template.Categories;
    [arr[index], arr[newIndex]] = [arr[newIndex], arr[index]];

    arr[index].ApplicationSortOrder = index - num;
  }

  private resortReviewer(index: number, newIndex: number) {
    this.resortReviewerBelow(index, newIndex);
    this.resortReviewerAbove(index, newIndex);
  }

  private resortReviewerBelow(index: number, newIndex: number) {
    let categories: ApplicationCategoryModel[] = this.template.Categories
      .filter(x => x.ReviewerSortOrder >= newIndex && x.ReviewerSortOrder < index);
    for(let category of categories) {
      category.ReviewerSortOrder++;
    }
  }

  private resortReviewerAbove(index: number, newIndex: number) {
    let categories: ApplicationCategoryModel[] = this.template.Categories
      .filter(x => x.ReviewerSortOrder <= newIndex && x.ReviewerSortOrder > index);
    for(let category of categories) {
      category.ReviewerSortOrder--;
    }
  }

  private resortApplicationAbove(index: number) {
    let categories: ApplicationCategoryModel[] = this.template.Categories
      .filter(x => x.ApplicationSortOrder > index);
    for(let category of categories) {
      category.ApplicationSortOrder--;
    }
  }
}
