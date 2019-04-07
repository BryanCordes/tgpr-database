import {ApplicationTypeModel} from "./application-type.model";
import {ApplicationCategoryModel} from "./application-category.model";

export class ApplicationModel {
    ApplicationId: number;
    Type: ApplicationTypeModel;
    Categories: ApplicationCategoryModel[];
}