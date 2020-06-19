import {Component, Input, OnInit} from '@angular/core';

import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';
import {FormGroup} from '@angular/forms';
import {QuestionTypeEnum} from '../../../../_models/administration/applications/question-type.enum';
import {ApplicationQuestionAnswerModel} from '../../../../_models/applications/application-question-answer.model';

@Component({
  selector: 'tgpr-application-administration-test-question',
  templateUrl: './test-application-template-question.component.html'
})
export class TestApplicationTemplateQuestionComponent {

  @Input() question: ApplicationQuestionModel;
  @Input() formGroup: FormGroup;
  @Input() questionNumber: number;
  @Input() answers: ApplicationQuestionAnswerModel[];

  public questionType = QuestionTypeEnum;
}
