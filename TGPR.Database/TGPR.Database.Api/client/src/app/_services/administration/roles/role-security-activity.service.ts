import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RoleSecurityActivityModel} from '../../../_models/administration/users/role-security-activity.model';
import {environment} from '../../../../environments/environment';
import {Observable} from 'rxjs/internal/Observable';

@Injectable()
export class RoleSecurityActivityService {

  constructor(private _http: HttpClient) { }

  public add(roleId: string, securityActivityId: number): Observable<RoleSecurityActivityModel> {
    const roleSecurityActivity: RoleSecurityActivityModel = {
      RoleId: roleId,
      SecurityActivityId: securityActivityId
    };

    return this._http.post<RoleSecurityActivityModel>(environment.baseUrl + '/api/RoleSecurityActivity/add', roleSecurityActivity);
  }

  public remove(roleSecurityActivityId: string): Observable<void> {
    return this._http.delete<void>(environment.baseUrl + `/api/RoleSecurityActivity/${roleSecurityActivityId}/remove`);
  }
}
