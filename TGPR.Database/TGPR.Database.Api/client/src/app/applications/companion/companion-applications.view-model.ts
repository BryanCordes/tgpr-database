import {Injectable} from '@angular/core';
import {CompanionApplicationDataSource} from './companion-application.data-source';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {ApplicationReviewService} from '../../_services/applications/application-review.service';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {ApplicationModel} from '../../_models/applications/application.model';
import {ApplicationStatusEnum} from '../../_models/applications/application-status.enum';

@Injectable()
export class CompanionApplicationsViewModel {

  public dataSource: CompanionApplicationDataSource;
  public columns: string[];

  private dataSourceFilter: DataSourceFilter;

  constructor(private _service: ApplicationReviewService) {
    this.initializeColumns();
    this.initializeFilter();
  }

  public initializeDataSource(applications: DataSourceResponse<ApplicationModel[]>) {
    this.dataSource = new CompanionApplicationDataSource(this._service);

    this.dataSource.setApplications(applications);
  }

  public updateDataSource(page: number, pageSize: number, sort: string, direction: string, filter: string) {
    this.dataSourceFilter.Page = page;
    this.dataSourceFilter.PageSize = pageSize;

    if(!sort) {
      sort = "ApplicationId";
    }

    this.dataSourceFilter.SortColumn = sort;

    if(!direction) {
      direction = "asc";
    }

    this.dataSourceFilter.SortDirection = direction;
    this.dataSourceFilter.Filter = filter;

    this.dataSource.loadApplications(this.dataSourceFilter);
  }

  public getStatus(application: ApplicationModel) {
    switch(application.ApplicationStatusId) {
      case ApplicationStatusEnum.New:
        return "New";
      case ApplicationStatusEnum.InProgress:
        return "In Progress";
      case ApplicationStatusEnum.Approved:
        return "Approved";
      case ApplicationStatusEnum.Closed:
        return "Closed";
      case ApplicationStatusEnum.DoNotAdopt:
        return "Do Not Adopt";
      default:
        return "New";
    }
  }

  private initializeColumns() {
    this.columns = [
      "ApplicationId",
      "ApplicationStatusId",
      "FirstName",
      "LastName",
      "Email",
      "Phone",
      "Address",
      "City",
      "State",
      "Zip",
      "CreatedOn",
      "UpdatedOn",
      "delete",
      "edit"
    ];
  }

  private initializeFilter() {
    this.dataSourceFilter = {
      Filter: "",
      SortColumn: "ApplicationId",
      SortDirection: "asc",
      Page: 0,
      PageSize: 25
    };
  }
}
