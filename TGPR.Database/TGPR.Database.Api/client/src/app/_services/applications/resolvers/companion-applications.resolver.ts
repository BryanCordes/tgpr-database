import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {DataSourceResponse} from '../../../_models/shared/datasource-response';
import {DataSourceFilter} from '../../../_models/shared/datasource-filter';
import {Observable} from 'rxjs/internal/Observable';
import {ApplicationModel} from '../../../_models/applications/application.model';
import {ApplicationReviewService} from '../application-review.service';

@Injectable()
export class CompanionApplicationsResolver implements Resolve<DataSourceResponse<ApplicationModel[]>> {
  private dataSourceFilter: DataSourceFilter = {
    Filter: "",
    SortColumn: "ApplicationId",
    SortDirection: "asc",
    Page: 0, // 0 based
    PageSize: 25
  };

  constructor(private _service: ApplicationReviewService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DataSourceResponse<ApplicationModel[]>> | Promise<DataSourceResponse<ApplicationModel[]>> | DataSourceResponse<ApplicationModel[]> {
    return this._service.getCompanion(this.dataSourceFilter);
  }
}
