import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {QuestionTypeMultiViewModel} from './question-type-multi.view-model';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {ApplicationOptionModel} from '../../../../../../_models/applications/application-option.model';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-question-type-multi',
  templateUrl: './question-type-multi.component.html',
  providers: [QuestionTypeMultiViewModel]
})
export class QuestionTypeMultiComponent implements OnInit, OnDestroy {
  @Input() question: ApplicationQuestionModel;
  @Input() formGroup: FormGroup;
  @Input() answers: ApplicationQuestionAnswerModel[];

  @ViewChild('childQuestions') childQuestionsComponent;

  constructor(public _viewModel: QuestionTypeMultiViewModel) { }

  public toggleOption(option: ApplicationOptionModel, $event) {
    if($event.checked) {
      this._viewModel.addOption(option);
    } else {
      this._viewModel.removeOption(option);
    }

    this._viewModel.updateChildQuestions();

    if(this.childQuestionsComponent) {
      this.childQuestionsComponent.initializeQuestios();
    }
  }

  ngOnInit(): void {
    this._viewModel.question = this.question;
    this._viewModel.formGroup = this.formGroup;
    this._viewModel.answers = this.answers;
  }

  ngOnDestroy(): void {
    this._viewModel.removeFormControl();
  }
}
