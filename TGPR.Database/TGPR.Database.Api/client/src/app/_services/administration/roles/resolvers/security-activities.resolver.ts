import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/internal/Observable';
import {SecurityActivityService} from '../security-activity.service';
import {SecurityActivityModel} from '../../../../_models/administration/users/security-activity.model';

@Injectable()
export class SecurityActivitiesResolver implements Resolve<SecurityActivityModel[]> {
  constructor(private _service: SecurityActivityService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<SecurityActivityModel[]> | Promise<SecurityActivityModel[]> | SecurityActivityModel[] {
    return this._service.getSecurityActivities();
  }
}
