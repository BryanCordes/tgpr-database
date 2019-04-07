import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';
import {environment} from '../../../../environments/environment';
import {UserRoleModel} from '../../../_models/administration/users/user-role.model';

@Injectable()
export class UserRoleService {

  constructor(private _http: HttpClient) { }

  public add(userId: string, roleId: string): Observable<UserRoleModel> {
    const userRole: UserRoleModel = {
      UserId: userId,
      RoleId: roleId
    };

    return this._http.post<UserRoleModel>(environment.baseUrl + '/api/UserRole/add', userRole);
  }

  public remove(userRoleId: string): Observable<void> {
    return this._http.delete<void>(environment.baseUrl + `/api/UserRole/${userRoleId}/remove`);
  }
}
