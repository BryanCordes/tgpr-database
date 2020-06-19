import {Injectable} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ApplicationOptionModel} from '../../../../../../_models/applications/application-option.model';
import {resolvedPromise} from '../../../../../../_utilities/resolved-promise.utility';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Injectable()
export class QuestionTypeDropdownViewModel {
  public question: ApplicationQuestionModel;
  public formGroup: FormGroup;
  public answers: ApplicationQuestionAnswerModel[];

  public formControl: FormControl;

  public activeOption: ApplicationOptionModel;

  public addFormControl() {
    this.formControl = new FormControl('', Validators.required);

    resolvedPromise.then(() => {
      const inputName = `question_${this.question.ApplicationQuestionId}`;

      this.formGroup.addControl(inputName, this.formControl);
    });
  }

  public removeFormControl() {
    resolvedPromise.then(() => {
      const inputName = `question_${this.question.ApplicationQuestionId}`;

      this.formGroup.removeControl(inputName);
    });
  }

  public setOption(option: ApplicationOptionModel) {
    this.activeOption = option;

    let answer = this.answers.find(x => x.ApplicationQuestionId === option.ApplicationQuestionId);
    if(!answer) {
      answer = {
        ApplicationQuestionId: this.question.ApplicationQuestionId,
        ApplicationOptionId: this.activeOption.ApplicationOptionId
      };

      this.answers.push(answer);
    } else {
      answer.ApplicationOptionId = this.activeOption.ApplicationOptionId;
    }
  }
}
