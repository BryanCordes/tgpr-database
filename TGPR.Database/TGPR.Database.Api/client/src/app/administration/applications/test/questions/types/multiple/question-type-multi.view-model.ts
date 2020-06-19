import {Injectable} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../../../_models/applications/application-question.model';
import {FormControl, FormGroup} from '@angular/forms';
import {ApplicationOptionModel} from '../../../../../../_models/applications/application-option.model';
import {resolvedPromise} from '../../../../../../_utilities/resolved-promise.utility';
import {ApplicationQuestionAnswerModel} from '../../../../../../_models/applications/application-question-answer.model';

@Injectable()
export class QuestionTypeMultiViewModel {
  public question: ApplicationQuestionModel;
  public formGroup: FormGroup;
  public answers: ApplicationQuestionAnswerModel[];

  public formControl: FormControl;

  public showChildQuestions = false;
  public childQuestions: ApplicationQuestionModel[] = [];

  public activeOptions: ApplicationOptionModel[] = [];

  public addOption(option: ApplicationOptionModel) {
    this.activeOptions.push(option);

    let answer: ApplicationQuestionAnswerModel = {
      ApplicationQuestionId: this.question.ApplicationQuestionId,
      ApplicationOptionId: option.ApplicationOptionId
    };

    this.answers.push(answer);
  }

  public removeOption(option: ApplicationOptionModel) {
    const index: number = this.activeOptions.indexOf(option);
    this.activeOptions.splice(index, 1);

    let answer = this.answers.find(x => x.ApplicationOptionId === option.ApplicationOptionId);
    const answerIndex = this.answers.indexOf(answer);
    this.answers.splice(answerIndex, 1);
  }

  public removeFormControl() {
    resolvedPromise.then(() => {
      const inputName = `question_${this.question.ApplicationQuestionId}`;

      this.formGroup.removeControl(inputName);
    });
  }

  public updateChildQuestions() {

    let currentQuestions: ApplicationQuestionModel[] = [];

    for(const option of this.activeOptions) {

      if(!option.ChildQuestions) {
        continue;
      }

      currentQuestions = currentQuestions.concat(option.ChildQuestions);
    }

    // remove old
    const removedQuestions: ApplicationQuestionModel[] = [];
    for(const question of this.childQuestions) {
      const index: number = this.childQuestions.indexOf(question);
      if(index < 0) {
        continue;
      }

      removedQuestions.push(question);
    }

    for(const removedQuestion of removedQuestions) {
      const index: number = this.childQuestions.indexOf(removedQuestion);

      this.childQuestions.splice(index, 1);
    }

    for(const question of currentQuestions) {
      const index: number = this.childQuestions.indexOf(question);
      if(index >= 0) {
        continue;
      }

      this.childQuestions.push(question);
    }

    this.childQuestions.sort(this.compare);

    this.showChildQuestions = this.childQuestions.length > 0;
  }

  private compare(a: ApplicationQuestionModel, b: ApplicationQuestionModel) {
    if ( a.ParentApplicationOptionId < b.ParentApplicationOptionId ){
      return -1;
    }
    if ( a.ParentApplicationOptionId > b.ParentApplicationOptionId ){
      return 1;
    }
    return 0;
  }
}
