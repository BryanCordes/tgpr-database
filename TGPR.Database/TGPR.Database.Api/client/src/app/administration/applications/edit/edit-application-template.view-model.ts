import {Injectable} from '@angular/core';
import {ApplicationTemplateModel} from '../../../_models/administration/applications/application-template.model';
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';

@Injectable()
export class EditApplicationTemplateViewModel {

  public template: ApplicationTemplateModel;
  public questionTypes: ApplicationQuestionTypeModel[];

}
