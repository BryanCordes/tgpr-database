import {ApplicationOptionModel} from "./application-option.model";
import {ApplicationQuestionAnswerModel} from "./application-question-answer.model";

export class ApplicationQuestionModel {
    ApplicationQuestionId: number;
    ApplicationQuestionTypeId: number;
    ApplicationCategoryId: number;
    ParentApplicationOptionId?: number;
    Text: string;
    ApplicationSortOrder: number;
    ReviewerSortOrder: number;

    Options: ApplicationOptionModel[];
    Answer: ApplicationQuestionAnswerModel;
}