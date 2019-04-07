import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {UserAdministrationService} from '../../_services/administration/users/user-administration.service';
import {UserModel} from '../../_models/administration/users/user.model';

@Injectable()
export class UserAdministrationUserResolver implements Resolve<UserModel> {
  constructor(private _service: UserAdministrationService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserModel> | Promise<UserModel> | UserModel {
    let userId: string = route.paramMap.get('id');

    return this._service.get(userId);
  }
}
