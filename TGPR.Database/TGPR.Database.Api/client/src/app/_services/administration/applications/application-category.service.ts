import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {EditApplicationCategoryModel} from '../../../administration/applications/categories/edit/edit-application-category-dialog.component';

@Injectable()
export class ApplicationCategoryService {

    constructor(private http: HttpClient) { }

    create(applicationCategory: ApplicationCategoryModel) {
        return this.http.post<ApplicationCategoryModel>(environment.baseUrl + "/api/ApplicationCategory/create", applicationCategory);
    }

    update(applicationCategory: EditApplicationCategoryModel) {
        return this.http.put<ApplicationCategoryModel>(environment.baseUrl + `/api/ApplicationCategory/${applicationCategory.ApplicationCategoryId}`, applicationCategory);
    }

    delete(applicationCategory: ApplicationCategoryModel) {
        return this.http.delete(environment.baseUrl + `/api/ApplicationCategory/${applicationCategory.ApplicationCategoryId}`);
    }

    updateSortOrder(applicationCategory: ApplicationCategoryModel) {
        return this.http.post(environment.baseUrl + "/api/ApplicationCategory/updateSortOrder", applicationCategory);
    }
}
