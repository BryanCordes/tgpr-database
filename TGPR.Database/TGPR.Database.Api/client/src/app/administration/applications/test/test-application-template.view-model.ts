import {Injectable} from '@angular/core';
import {ApplicationTemplateModel} from '../../../_models/administration/applications/application-template.model';
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ApplicationQuestionAnswerModel} from '../../../_models/applications/application-question-answer.model';
import {ApplicationModel} from '../../../_models/applications/application.model';
import {ApplicationService} from '../../../_services/administration/applications/application.service';

export interface StateDisplay {
  Value: string;
  Text: string;
}

@Injectable()
export class TestApplicationTemplateViewModel {

  public template: ApplicationTemplateModel;
  public questions: ApplicationQuestionModel[];
  public formGroup: FormGroup;
  public answers: ApplicationQuestionAnswerModel[] = [];

  public states: StateDisplay[];

  constructor(private _service: ApplicationService, private _router: Router) {
    this.initializeStates();
  }

  public createFormGroup() : FormGroup {
    let formGroup = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      address2: new FormControl(''),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      zip: new FormControl('', Validators.required)
    });

    return formGroup;
  }

  public initializeQuestions() {
    this.questions = [];

    for (const category of this.template.Categories) {

      if (!category.Questions) {
        continue;
      }

      this.initializeQuestionAnswers(category.Questions);

      //this.questions = this.questions.concat(category.Questions);
    }
  }

  public save() {
    let application: ApplicationModel = {
      ApplicationTemplateId: this.template.ApplicationTemplateId,
      ApplicationStatusId: 1, // new
      FirstName: this.formGroup.controls['firstName'].value,
      LastName: this.formGroup.controls['lastName'].value,
      Email: this.formGroup.controls['email'].value,
      Phone: this.formGroup.controls['phone'].value,
      Address: this.formGroup.controls['address'].value,
      Address2: this.formGroup.controls['address2'].value,
      City: this.formGroup.controls['city'].value,
      State: this.formGroup.controls['state'].value.Value,
      Zip: this.formGroup.controls['zip'].value,
      IsTest: true,
      QuestionAnswers: this.answers
    };

    this._service.create(application)
      .subscribe(created => {
        this._router.navigate(['administration', 'applications']);
      });
  }

  public cancel() {
    this._router.navigate(['administration', 'applications']);
  }

  private initializeQuestionAnswers(questions: ApplicationQuestionModel[]) {
    for (const question of questions) {
      question.Answers = [];

      if (!question.Options) {
        continue;
      }

      for(const option of question.Options) {
        if (!option.ChildQuestions) {
          continue;
        }

        this.initializeQuestionAnswers(option.ChildQuestions);
      }
    }
  }

  private initializeStates() {
    this.states = [
      {Value: "AK", Text: "Alabama"},
      {Value: "AL", Text: "Alaska"},
      {Value: "AZ", Text: "Arizona"},
      {Value: "AR", Text: "Arkansas"},
      {Value: "CA", Text: "California"},
      {Value: "CO", Text: "Colorado"},
      {Value: "CT", Text: "Connecticut"},
      {Value: "DC", Text: "District of Columbia"},
      {Value: "DE", Text: "Delaware"},
      {Value: "FL", Text: "Florida"},
      {Value: "GA", Text: "Georgia"},
      {Value: "HI", Text: "Hawaii"},
      {Value: "ID", Text: "Idaho"},
      {Value: "IL", Text: "Illinois"},
      {Value: "IN", Text: "Indiana"},
      {Value: "IA", Text: "Iowa"},
      {Value: "KS", Text: "Kansas"},
      {Value: "KY", Text: "Kentucky"},
      {Value: "LA", Text: "Louisiana"},
      {Value: "ME", Text: "Maine"},
      {Value: "MD", Text: "Maryland"},
      {Value: "MA", Text: "Massachusetts"},
      {Value: "MI", Text: "Michigan"},
      {Value: "MN", Text: "Minnesota"},
      {Value: "MS", Text: "Mississippi"},
      {Value: "MO", Text: "Missouri"},
      {Value: "MT", Text: "Montana"},
      {Value: "NE", Text: "Nebraska"},
      {Value: "NV", Text: "Nevada"},
      {Value: "NH", Text: "New Hampshire"},
      {Value: "NJ", Text: "New Jersey"},
      {Value: "NM", Text: "New Mexico"},
      {Value: "NY", Text: "New York"},
      {Value: "NC", Text: "North Carolina"},
      {Value: "ND", Text: "North Dakota"},
      {Value: "OH", Text: "Ohio"},
      {Value: "OK", Text: "Oklahoma"},
      {Value: "OR", Text: "Oregon"},
      {Value: "PA", Text: "Pennsylvania"},
      {Value: "RI", Text: "Rhode Island"},
      {Value: "SC", Text: "South Carolina"},
      {Value: "SD", Text: "South Dakota"},
      {Value: "TN", Text: "Tennessee"},
      {Value: "TX", Text: "Texas"},
      {Value: "UT", Text: "Utah"},
      {Value: "VT", Text: "Vermont"},
      {Value: "VA", Text: "Virginia"},
      {Value: "WA", Text: "Washington"},
      {Value: "WV", Text: "West Virginia"},
      {Value: "WI", Text: "Wisconsin"},
      {Value: "WY", Text: "Wyoming"}
    ];
  }
}
