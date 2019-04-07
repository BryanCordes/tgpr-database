import {ApplicationTypeModel} from "../../applications/application-type.model";
import {ApplicationCategoryModel} from "../../applications/application-category.model";

export class ApplicationTemplateModel {
    ApplicationTemplateId?: number;
    Name: string;
    ApplicationTypeId: number;
    Type: ApplicationTypeModel;
    Active: boolean;
    Deleted: boolean;
    Categories: ApplicationCategoryModel[];
    CreatedOn: Date;
    UpdatedOn: Date;
}
