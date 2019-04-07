import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RoleModel} from '../../../_models/administration/users/role.model';
import {environment} from '../../../../environments/environment';

@Injectable()
export class RoleService {

  constructor(private _http: HttpClient) { }

  getRoles() {
    return this._http.get<RoleModel[]>(environment.baseUrl + '/api/Role');
  }

  getEditableRoles() {
    return this._http.get<RoleModel[]>(environment.baseUrl + '/api/Role/users');
  }

  create(role: RoleModel) {
    return this._http.post<RoleModel>(environment.baseUrl + '/api/Role', role);
  }

  edit(role: RoleModel) {
    return this._http.put<RoleModel>(environment.baseUrl + '/api/Role', role);
  }

  delete(role: RoleModel) {
    return this._http.delete(environment.baseUrl + `/api/Role/${role.RoleId}`);
  }
}
