import {DataSourceResponse} from '../../../../_models/shared/datasource-response';
import {Observable} from 'rxjs/internal/Observable';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {DataSourceFilter} from '../../../../_models/shared/datasource-filter';
import {Injectable} from '@angular/core';
import {UserAdministrationService} from '../user-administration.service';
import {UserSummaryModel} from '../../../../_models/administration/users/user-summary.model';

@Injectable()
export class UserAdministrationUsersResolver implements Resolve<DataSourceResponse<UserSummaryModel[]>> {
  private dataSourceFilter: DataSourceFilter = {
    Filter: "",
    SortColumn: "Email",
    SortDirection: "asc",
    Page: 0, // 0 based
    PageSize: 25
  };

  constructor(private _service: UserAdministrationService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DataSourceResponse<UserSummaryModel[]>> | Promise<DataSourceResponse<UserSummaryModel[]>> | DataSourceResponse<UserSummaryModel[]> {
    return this._service.getUsers(this.dataSourceFilter);
  }
}
