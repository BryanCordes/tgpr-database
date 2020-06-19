import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {QuestionTypeSingleViewModel} from './question-type-single.view-model';
import {ApplicationOptionModel} from '../../../../../../_models/applications/application-option.model';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-question-type-single',
  templateUrl: './question-type-single.component.html',
  providers: [QuestionTypeSingleViewModel]
})
export class QuestionTypeSingleComponent implements OnInit, OnDestroy {

  @Input() question: ApplicationQuestionModel;
  @Input() formGroup: FormGroup;
  @Input() answers: ApplicationQuestionAnswerModel[];

  constructor(public _viewModel: QuestionTypeSingleViewModel) { }

  public setOption(option: ApplicationOptionModel) {
    this._viewModel.setOption(option);
  }

  ngOnInit(): void {
    this._viewModel.question = this.question;
    this._viewModel.formGroup = this.formGroup;
    this._viewModel.answers = this.answers;

    this._viewModel.addFormControl();
  }

  ngOnDestroy(): void {
    this._viewModel.removeFormControl();
  }
}
