export class ApplicationCategoryReviewModel {
    ApplicationCategoryReviewId: number;
    ApplicationCategoryId: number;
    ApplicationCategoryReviewStatusId: number;
    ReviewedById: number;
    ReviewedOn: Date;
    Text: string;
    History: ApplicationCategoryReviewModel[]
}