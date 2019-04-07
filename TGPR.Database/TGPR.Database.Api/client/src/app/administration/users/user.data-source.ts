import {BehaviorSubject, Observable, of} from 'rxjs/index';
import {catchError, finalize} from "rxjs/internal/operators";
import {DataSource} from '@angular/cdk/table';
import {CollectionViewer} from '@angular/cdk/collections';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {UserAdministrationService} from '../../_services/administration/users/user-administration.service';
import {UserSummaryModel} from '../../_models/administration/users/user-summary.model';


export class UserDataSource extends DataSource<UserSummaryModel> {

  private users = new BehaviorSubject<UserSummaryModel[]>([]);

  private loading = new BehaviorSubject<boolean>(false);

  public data: UserSummaryModel[] = [];
  public total: number;

  public loading$ = this.loading.asObservable();

  constructor(private _service: UserAdministrationService) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<UserSummaryModel[]> {
    return this.users.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.users.complete();
    this.loading.complete();
  }

  setUsers(data: DataSourceResponse<UserSummaryModel[]>) {
    this.data = data.Data;
    this.total = data.TotalRecords;

    this.users.next(data.Data);
  }

  loadUsers(dataSourceFilter: DataSourceFilter) {
    let me = this;

    me.loading.next(true);

    me._service.getUsers(dataSourceFilter)
      .pipe(
        catchError(() => of([])),
        finalize(() => me.loading.next(false))
      )
      .subscribe(response => {
        let applications = me.getUsers(response);

        me.data = applications;

        me.users.next(me.getUsers(response))
      });
  }

  private getUsers(response: any): UserSummaryModel[] {
    let dataSourceResponse = response as DataSourceResponse<UserSummaryModel[]>;

    this.total = dataSourceResponse.TotalRecords;
    console.log(this.total);

    return dataSourceResponse.Data;
  }
}
