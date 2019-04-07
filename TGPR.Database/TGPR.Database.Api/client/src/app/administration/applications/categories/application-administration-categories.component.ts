import {Component, Input, OnInit} from '@angular/core';
import {ApplicationTemplateModel} from '../../../_models/administration/applications/application-template.model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';
import {ApplicationAdministrationCategoriesViewModel} from './application-administration-categories.view-model';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';

@Component({
  selector: 'tgpr-application-administration-categories',
  templateUrl: './application-administration-categories.component.html',
  providers: [ApplicationAdministrationCategoriesViewModel]
})
export class ApplicationAdministrationCategoriesComponent implements OnInit {

  @Input() template: ApplicationTemplateModel;
  @Input() questionTypes: ApplicationQuestionTypeModel[];

  constructor(public _viewModel: ApplicationAdministrationCategoriesViewModel) { }

  public create() {
    this._viewModel.create();
  }

  public moveUp(category: ApplicationCategoryModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveUp(category);
  }

  public moveDown(category: ApplicationCategoryModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveDown(category);
  }

  public edit(category: ApplicationCategoryModel, $event) {
    $event.stopPropagation();

    this._viewModel.edit(category);
  }

  public delete(category: ApplicationCategoryModel) {
    this._viewModel.delete(category);
  }

  ngOnInit(): void {
    this._viewModel.template = this.template;
    this._viewModel.questionTypes = this.questionTypes;
  }
}
