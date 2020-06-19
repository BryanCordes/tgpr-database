import {ApplicationQuestionAnswerModel} from './application-question-answer.model';
import {ApplicationTemplateModel} from '../administration/applications/application-template.model';
import {ApplicationCategoryReviewModel} from './application-category-review.model';

export class ApplicationModel {
    ApplicationId?: number;
    ApplicationTemplateId: number;
    ApplicationStatusId: number;

    FirstName: string;
    LastName: string;
    Email: string;
    Address: string;
    Address2?: string;
    City: string;
    State: string;
    Zip: string;
    Phone: string;

    IsTest: boolean;

    CreatedOn?: Date;
    UpdatedOn?: Date;

    ApplicationTemplate?: ApplicationTemplateModel;
    QuestionAnswers?: ApplicationQuestionAnswerModel[];
    ApplicationCategoryReviews?: ApplicationCategoryReviewModel[];
}
