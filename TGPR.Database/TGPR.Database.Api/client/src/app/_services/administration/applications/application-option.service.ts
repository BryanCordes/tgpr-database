import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {ApplicationOptionModel} from '../../../_models/applications/application-option.model';
import {EditApplicationOptionModel} from '../../../administration/applications/options/edit/edit-application-option-dialog.component';

@Injectable()
export class ApplicationOptionService {

    constructor(private http: HttpClient) { }

    create(applicationOption: ApplicationOptionModel) {
        return this.http.post<ApplicationOptionModel>(environment.baseUrl + "/api/ApplicationOption/create", applicationOption);
    }

    update(applicationOption: EditApplicationOptionModel) {
        return this.http.put<ApplicationOptionModel>(environment.baseUrl + `/api/ApplicationOption/${applicationOption.ApplicationOptionId}`, applicationOption);
    }

    updateSortOrder(applicationOption: ApplicationOptionModel) {
        return this.http.post(environment.baseUrl + "/api/ApplicationOption/updateSortOrder", applicationOption);
    }

    delete(applicationOption: ApplicationOptionModel) {
        return this.http.delete(environment.baseUrl + `/api/ApplicationOption/${applicationOption.ApplicationOptionId}`);
    }
}
