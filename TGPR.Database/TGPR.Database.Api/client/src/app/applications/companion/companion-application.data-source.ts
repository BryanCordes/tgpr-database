import {ApplicationModel} from '../../_models/applications/application.model';
import {BehaviorSubject} from 'rxjs/internal/BehaviorSubject';
import {catchError, finalize} from 'rxjs/operators';
import {of} from 'rxjs/internal/observable/of';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {CollectionViewer} from '@angular/cdk/collections';
import {Observable} from 'rxjs/internal/Observable';
import {ApplicationReviewService} from '../../_services/applications/application-review.service';

export class CompanionApplicationDataSource {
  private applications = new BehaviorSubject<ApplicationModel[]>([]);

  private loading = new BehaviorSubject<boolean>(false);

  public data: ApplicationModel[] = [];
  public total: number;

  public loading$ = this.loading.asObservable();

  constructor(private _service: ApplicationReviewService) { }

  connect(collectionViewer: CollectionViewer): Observable<ApplicationModel[]> {
    return this.applications.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.applications.complete();
    this.loading.complete();
  }

  setApplications(data: DataSourceResponse<ApplicationModel[]>) {
    this.data = data.Data;
    this.total = data.TotalRecords;

    this.applications.next(data.Data);
  }

  loadApplications(dataSourceFilter: DataSourceFilter) {
    let me = this;

    me.loading.next(true);

    me._service.getCompanion(dataSourceFilter)
      .pipe(
        catchError(() => of([])),
        finalize(() => me.loading.next(false))
      )
      .subscribe(response => {
        let applications = me.getApplications(response);

        me.data = applications;

        me.applications.next(me.getApplications(response));
      });
  }

  private getApplications(response: any): ApplicationModel[] {
    let dataSourceResponse = response as DataSourceResponse<ApplicationModel[]>;

    this.total = dataSourceResponse.TotalRecords;

    return dataSourceResponse.Data;
  }
}
