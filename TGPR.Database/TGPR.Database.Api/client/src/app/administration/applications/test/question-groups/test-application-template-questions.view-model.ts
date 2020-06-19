import {Injectable} from '@angular/core';
import {ApplicationQuestionModel} from '../../../../_models/applications/application-question.model';

@Injectable()
export class TestApplicationTemplateQuestionsViewModel {

  public questions: ApplicationQuestionModel[];
  public questionGroups: ApplicationQuestionModel[][] = [];

  public groupQuestions() {
    this.questionGroups = [];

    let totalWidth = 0;
    let questionGroup: ApplicationQuestionModel[] = [];

    let count = 1;

    for (const question of this.questions) {

      question.Count = count++;

      if(totalWidth + question.Width === 12) {
        questionGroup.push(question);

        const finalGroup = questionGroup.slice(0);
        this.questionGroups.push(finalGroup);

        totalWidth = 0;
        questionGroup = [];

        continue;
      }

      if(totalWidth + question.Width > 12) {
        const finalGroup = questionGroup.slice(0);
        this.questionGroups.push(finalGroup);

        totalWidth = 0;
        questionGroup = [];

        continue;
      }

      questionGroup.push(question);
      totalWidth += question.Width;
    }
  }
}
