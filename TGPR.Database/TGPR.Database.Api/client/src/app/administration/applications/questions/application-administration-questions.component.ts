import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ApplicationAdministrationQuestionsViewModel} from './application-administration-questions.view-model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {ApplicationOptionModel} from '../../../_models/applications/application-option.model';
import {MatExpansionPanel} from '@angular/material';

@Component({
  selector: 'tgpr-application-administration-questions',
  templateUrl: './application-administration-questions.component.html',
  providers: [ApplicationAdministrationQuestionsViewModel]
})
export class ApplicationAdministrationQuestionsComponent implements OnInit {

  @Input() category: ApplicationCategoryModel;
  @Input() option: ApplicationOptionModel;
  @Input() questionTypes: ApplicationQuestionTypeModel[];

  @ViewChild(MatExpansionPanel) expansionPanel: MatExpansionPanel;

  constructor(public _viewModel: ApplicationAdministrationQuestionsViewModel) { }

  public create() {
    this._viewModel.create();
  }

  public moveUp(question: ApplicationQuestionModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveUp(question);
  }

  public moveDown(question: ApplicationQuestionModel, $event) {
    $event.stopPropagation();

    this._viewModel.moveDown(question);
  }

  public edit(question: ApplicationQuestionModel, $event) {
    $event.stopPropagation();

    this._viewModel.edit(question);
  }

  public delete(question: ApplicationQuestionModel) {
    this._viewModel.delete(question);
  }

  expand(question: ApplicationQuestionModel) {
    if(!this._viewModel.hasEditableOptions(question)) {
      this.expansionPanel.close();
    }
  }

  ngOnInit(): void {
    this._viewModel.category = this.category;
    this._viewModel.option = this.option;
    this._viewModel.questionTypes = this.questionTypes;

    this._viewModel.load();
  }
}
