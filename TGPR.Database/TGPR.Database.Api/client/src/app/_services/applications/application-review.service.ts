import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApplicationModel} from '../../_models/applications/application.model';
import {environment} from '../../../environments/environment';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {Observable} from 'rxjs/internal/Observable';
import {DataSourceResponse} from '../../_models/shared/datasource-response';


@Injectable()
export class ApplicationReviewService {

  constructor(private http: HttpClient) { }

  getCompanion(filter: DataSourceFilter): Observable<DataSourceResponse<ApplicationModel[]>> {
    return this.http.post<DataSourceResponse<ApplicationModel[]>>(environment.baseUrl + "/api/Application/companion", filter);
  }

  create(application: ApplicationModel) {
    return this.http.post<ApplicationModel>(environment.baseUrl + "/api/Application/create", application);
  }
}
