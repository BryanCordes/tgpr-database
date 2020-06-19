import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {RoleModel} from '../../../../_models/administration/users/role.model';
import {RoleService} from '../role.service';
import {Observable} from 'rxjs/internal/Observable';

@Injectable()
export class RolesResolver implements Resolve<RoleModel[]> {
  constructor(private _service: RoleService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<RoleModel[]> | Promise<RoleModel[]> | RoleModel[] {
    return this._service.getRoles();
  }
}
