import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {QuestionTypeTextViewModel} from './question-type-text.view-model';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-question-type-text',
  templateUrl: './question-type-text.component.html',
  providers: [QuestionTypeTextViewModel]
})
export class QuestionTypeTextComponent implements OnInit, OnDestroy {

  @Input() question: ApplicationQuestionModel;
  @Input() formGroup: FormGroup;
  @Input() answers: ApplicationQuestionAnswerModel[];

  constructor(public _viewModel: QuestionTypeTextViewModel) { }

  public setText($event) {
    let value: string = $event.target.value;

    this._viewModel.setText(value);
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
