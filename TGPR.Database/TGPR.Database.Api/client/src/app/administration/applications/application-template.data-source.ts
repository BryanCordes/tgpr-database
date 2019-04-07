import {ApplicationTemplateSummaryModel} from '../../_models/administration/applications/application-template-summary.model';
import {BehaviorSubject} from 'rxjs/internal/BehaviorSubject';
import {DataSource} from '@angular/cdk/table';
import {ApplicationTemplateService} from '../../_services/administration/applications/application-template.service';
import {CollectionViewer} from '@angular/cdk/collections';
import {Observable} from 'rxjs/internal/Observable';
import {catchError, finalize} from 'rxjs/operators';
import {of} from 'rxjs/internal/observable/of';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';

export class ApplicationTemplateDataSource extends DataSource<ApplicationTemplateSummaryModel> {

  private applications = new BehaviorSubject<ApplicationTemplateSummaryModel[]>([]);

  private loading = new BehaviorSubject<boolean>(false);

  public data: ApplicationTemplateSummaryModel[] = [];
  public total: number;

  public loading$ = this.loading.asObservable();

  constructor(private _service: ApplicationTemplateService) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<ApplicationTemplateSummaryModel[]> {
    return this.applications.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.applications.complete();
    this.loading.complete();
  }

  setApplications(data: DataSourceResponse<ApplicationTemplateSummaryModel[]>) {
    this.data = data.Data;
    this.total = data.TotalRecords;

    this.applications.next(data.Data);
  }

  loadApplications(dataSourceFilter: DataSourceFilter) {
    let me = this;

    me.loading.next(true);

    me._service.getApplications(dataSourceFilter)
      .pipe(
        catchError(() => of([])),
        finalize(() => me.loading.next(false))
      )
      .subscribe(response => {
        let applications = me.getApplications(response);

        me.data = applications;

        me.applications.next(me.getApplications(response))
      });
  }

  private getApplications(response: any): ApplicationTemplateSummaryModel[] {
    let dataSourceResponse = response as DataSourceResponse<ApplicationTemplateSummaryModel[]>;

    this.total = dataSourceResponse.TotalRecords;
    console.log(this.total);

    return dataSourceResponse.Data;
  }
}
