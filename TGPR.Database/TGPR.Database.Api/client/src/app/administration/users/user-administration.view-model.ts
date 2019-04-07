import {Injectable} from '@angular/core';
import {UserDataSource} from './user.data-source';
import {UserAdministrationService} from '../../_services/administration/users/user-administration.service';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {UserSummaryModel} from '../../_models/administration/users/user-summary.model';
import {Router} from '@angular/router';

@Injectable()
export class UserAdministrationViewModel {

  public dataSource: UserDataSource;
  public columns: string[];
  public dataSourceFilter: DataSourceFilter;

  constructor(private _service: UserAdministrationService, private _router: Router) {
    this.initializeColumns();
    this.initializeDataSourceFilter();
  }

  public initializeDataSource(dataSourceResponse: DataSourceResponse<UserSummaryModel[]>) {
    this.dataSource = new UserDataSource(this._service);
    this.dataSource.setUsers(dataSourceResponse);
  }

  public updateDataSource(page: number, pageSize: number, sort: string, direction: string, filter: string) {
    this.dataSourceFilter.Page = page;
    this.dataSourceFilter.PageSize = pageSize;
    this.dataSourceFilter.SortColumn = sort;
    this.dataSourceFilter.SortDirection = direction;
    this.dataSourceFilter.Filter = filter;

    this.dataSource.loadUsers(this.dataSourceFilter);
  }

  public create() {
    this._router.navigate(['administration', 'users', 'create']);
  }

  public edit(user: UserSummaryModel) {
    this._router.navigate(['administration', 'user', user.UserId]);
  }

  public setActive(user: UserSummaryModel) {
    this._service.activate(user.UserId)
      .subscribe(() => {
        user.InactiveOn = undefined;
      });
  }

  public setInactive(user: UserSummaryModel) {
    this._service.inactivate(user.UserId)
      .subscribe(inactiveDate => {
        user.InactiveOn = inactiveDate;
      });
  }

  private initializeColumns() {
    this.columns = [
      "Email",
      "LastName",
      "FirstName",
      "PhoneNumber",
      "Address",
      "CreatedOn",
      "LastLoginOn",
      "InactiveOn",
      "inactive",
      "edit"
    ];
  }

  private initializeDataSourceFilter() {
    this.dataSourceFilter =   {
      Filter: "",
      SortColumn: "Email",
      SortDirection: "asc",
      Page: 0, // 0 based
      PageSize: 25
    };
  }
}
