import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {DataSourceResponse} from '../../../../_models/shared/datasource-response';
import {DataSourceFilter} from '../../../../_models/shared/datasource-filter';
import {ApplicationTemplateService} from '../application-template.service';
import {ApplicationTemplateSummaryModel} from '../../../../_models/administration/applications/application-template-summary.model';

@Injectable()
export class ApplicationAdministrationTemplateResolver implements Resolve<DataSourceResponse<ApplicationTemplateSummaryModel[]>> {
    private dataSourceFilter: DataSourceFilter = {
        Filter: "",
        SortColumn: "Name",
        SortDirection: "asc",
        Page: 0, // 0 based
        PageSize: 25
    };

    constructor(private _service: ApplicationTemplateService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DataSourceResponse<ApplicationTemplateSummaryModel[]>> | Promise<DataSourceResponse<ApplicationTemplateSummaryModel[]>> | DataSourceResponse<ApplicationTemplateSummaryModel[]> {
       return this._service.getApplications(this.dataSourceFilter);
    }
}
