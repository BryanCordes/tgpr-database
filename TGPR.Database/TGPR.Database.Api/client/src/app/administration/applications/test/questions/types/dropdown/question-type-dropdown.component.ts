import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {QuestionTypeDropdownViewModel} from './question-type-dropdown.view-model';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-question-type-dropdown',
  templateUrl: './question-type-dropdown.component.html',
  providers: [QuestionTypeDropdownViewModel]
})
export class QuestionTypeDropdownComponent implements OnInit, OnDestroy {

  @Input() question: ApplicationQuestionModel;
  @Input() formGroup: FormGroup;
  @Input() answers: ApplicationQuestionAnswerModel[];

  constructor(public _viewModel: QuestionTypeDropdownViewModel) { }

  public setOption($event) {
    let option = $event.value;

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
