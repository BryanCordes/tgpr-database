import {ApplicationQuestionModel} from "./application-question.model";
import {ApplicationCategoryReviewModel} from "./application-category-review.model";

export class ApplicationCategoryModel {
    ApplicationCategoryId: number;
    ApplicationTemplateId: number;
    Name: string;
    ApplicationSortOrder: number;
    ReviewerSortOrder: number;
    Deleted: boolean;
    HasReview: boolean;

    Questions: ApplicationQuestionModel[];

    Review: ApplicationCategoryReviewModel;
}