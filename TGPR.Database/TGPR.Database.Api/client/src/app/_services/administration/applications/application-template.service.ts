import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {DataSourceFilter} from '../../../_models/shared/datasource-filter';
import {DataSourceResponse} from '../../../_models/shared/datasource-response';
import {environment} from '../../../../environments/environment';
import {ApplicationTypeModel} from '../../../_models/applications/application-type.model';
import {ApplicationTemplateSummaryModel} from '../../../_models/administration/applications/application-template-summary.model';
import {ApplicationTemplateModel} from '../../../_models/administration/applications/application-template.model';

@Injectable()
export class ApplicationTemplateService {

    constructor(private http: HttpClient) { }

    getApplications(dataSourceFilter: DataSourceFilter) {
        return this.http.post<DataSourceResponse<ApplicationTemplateSummaryModel[]>>(environment.baseUrl + "/api/ApplicationTemplate/summary", dataSourceFilter);
    }

    getApplication(applicationTemplateId: number) {
        return this.http.get<ApplicationTemplateModel>(environment.baseUrl + `/api/ApplicationTemplate/${applicationTemplateId}`);
    }

    create(template: ApplicationTemplateModel) {
        return this.http.post<ApplicationTemplateModel>(environment.baseUrl + "/api/ApplicationTemplate/create", template);
    }

    updateName(template: ApplicationTemplateModel) {
        return this.http.put<void>(environment.baseUrl + `/api/ApplicationTemplate/name/${template.ApplicationTemplateId}`, template);
    }

    delete(summary: ApplicationTemplateSummaryModel) {
        return this.http.delete(environment.baseUrl + `/api/ApplicationTemplate/${summary.ApplicationTemplateId}`);
    }

    getApplicationTypes() {
        return this.http.get<ApplicationTypeModel[]>(environment.baseUrl + "/api/ApplicationType");
    }

    setActive(summary: ApplicationTemplateSummaryModel) {
        return this.http.get(environment.baseUrl + `/api/ApplicationTemplate/active/${summary.ApplicationTemplateId}`);
    }
}
