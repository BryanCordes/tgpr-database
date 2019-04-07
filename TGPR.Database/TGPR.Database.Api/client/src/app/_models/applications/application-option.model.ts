import {ApplicationQuestionModel} from "./application-question.model";

export class ApplicationOptionModel {
    ApplicationOptionId: number;
    ApplicationQuestionId: number;
    ApplicationSortOrder: number;
    ApplicationOptionStatusId: number;
    ReviewerSortOrder: number;
    Text: string;
    ChildQuestions: ApplicationQuestionModel[];
}