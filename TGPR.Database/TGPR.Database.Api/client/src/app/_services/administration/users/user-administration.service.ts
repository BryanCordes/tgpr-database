import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {DataSourceFilter} from '../../../_models/shared/datasource-filter';
import {DataSourceResponse} from '../../../_models/shared/datasource-response';
import {UserSummaryModel} from '../../../_models/administration/users/user-summary.model';
import {UserModel} from '../../../_models/administration/users/user.model';
import {Observable} from 'rxjs/internal/Observable';

@Injectable()
export class UserAdministrationService {

  constructor(private _http: HttpClient) { }

  getUsers(filter: DataSourceFilter): Observable<DataSourceResponse<UserSummaryModel[]>> {
    return this._http.post<DataSourceResponse<UserSummaryModel[]>>(environment.baseUrl + '/api/User/summary', filter);
  }

  get(userId: string): Observable<UserModel> {
    return this._http.get<UserModel>(environment.baseUrl + `/api/User/${userId}`);
  }

  create(user: UserModel): Observable<UserModel> {
    return this._http.post<UserModel>(environment.baseUrl + '/api/User', user);
  }

  update(user: UserModel) : Observable<void> {
    return this._http.put<void>(environment.baseUrl + `/api/User/${user.UserId}`, user);
  }

  inactivate(userId: string) : Observable<Date> {
    return this._http.post<Date>(environment.baseUrl + `/api/User/${userId}/inactivate`, {});
  }

  activate(userId: string) : Observable<void> {
    return this._http.post<void>(environment.baseUrl + `/api/User/${userId}/activate`, {});
  }
}
