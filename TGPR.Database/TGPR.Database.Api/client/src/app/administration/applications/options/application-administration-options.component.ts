import {Component, Input, OnInit} from '@angular/core';
import {ApplicationAdministrationOptionsViewModel} from './application-administration-options.view-model';
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {OptionStatusEnum} from '../../../_models/administration/applications/option-status.enum';
import {ApplicationOptionModel} from '../../../_models/applications/application-option.model';

@Component({
  selector: 'tgpr-application-administration-options',
  templateUrl: './application-administration-options.component.html',
  providers: [ApplicationAdministrationOptionsViewModel]
})
export class ApplicationAdministrationOptionsComponent implements OnInit {

  @Input() category: ApplicationCategoryModel;
  @Input() question: ApplicationQuestionModel;
  @Input() questionTypes: ApplicationQuestionTypeModel[];

  public status = OptionStatusEnum;

  constructor(public _viewModel: ApplicationAdministrationOptionsViewModel) { }

  public create() {
    this._viewModel.create();
  }

  public moveUp(option: ApplicationOptionModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveUp(option);
  }

  public moveDown(option: ApplicationOptionModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveDown(option);
  }

  public edit(option: ApplicationOptionModel, $event) {
    $event.stopPropagation();

    this._viewModel.edit(option);
  }

  public delete(option: ApplicationOptionModel) {
    this._viewModel.delete(option);
  }


  ngOnInit(): void {
    this._viewModel.category = this.category;
    this._viewModel.question = this.question;
    this._viewModel.questionTypes = this.questionTypes;
  }
}
