import {Component, Input, OnInit, QueryList, ViewChildren} from '@angular/core';
import {TestApplicationTemplateQuestionsViewModel} from './test-application-template-questions.view-model';
import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {TestApplicationTemplateQuestionComponent} from '../questions/test-application-template-question.component';
import {ApplicationQuestionAnswerModel} from '../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-application-administration-test-questions',
  templateUrl: './test-application-template-questions.component.html',
  providers: [TestApplicationTemplateQuestionsViewModel]
})
export class TestApplicationTemplateQuestionsComponent implements OnInit {

  @Input() questions: ApplicationQuestionModel[];
  @Input() formGroup: FormGroup;
  @Input() answers: ApplicationQuestionAnswerModel[];

  constructor(public _viewModel: TestApplicationTemplateQuestionsViewModel) { }

  public initializeQuestios() {
    this._viewModel.groupQuestions();
  }

  ngOnInit(): void {
    this._viewModel.questions = this.questions;

    this.initializeQuestios();
  }
}
