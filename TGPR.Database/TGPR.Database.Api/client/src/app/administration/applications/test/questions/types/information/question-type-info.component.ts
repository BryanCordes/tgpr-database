import {Component, Input} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';

@Component({
  selector: 'tgpr-question-type-info',
  templateUrl: './question-type-info.component.html'
})
export class QuestionTypeInfoComponent {
  @Input() question: ApplicationQuestionModel;
}
