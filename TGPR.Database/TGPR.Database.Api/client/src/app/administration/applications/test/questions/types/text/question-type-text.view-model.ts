import {Injectable} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {resolvedPromise} from '../../../../../../_utilities/resolved-promise.utility';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Injectable()
export class QuestionTypeTextViewModel {

  public formGroup: FormGroup;
  public question: ApplicationQuestionModel;
  public formControl: FormControl;
  public answers: ApplicationQuestionAnswerModel[];

  private answer: ApplicationQuestionAnswerModel;

  public addFormControl() {
    this.formControl = new FormControl('', Validators.required);

    this.answer = {
      ApplicationQuestionId: this.question.ApplicationQuestionId,
      Text: ''
    };

    this.answers.push(this.answer);

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

  public setText(value: string) {
    this.answer.Text = value;
  }

  public save() {
    const answer: ApplicationQuestionAnswerModel = {
      ApplicationQuestionId: this.question.ApplicationQuestionId,
      Text: this.formControl.value
    };

    this.question.Answers.push(answer);
  }
}
