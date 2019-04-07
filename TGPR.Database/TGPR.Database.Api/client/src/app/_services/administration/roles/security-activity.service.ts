import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {SecurityActivityModel} from '../../../_models/administration/users/security-activity.model';
import {Observable} from 'rxjs/internal/Observable';

@Injectable()
export class SecurityActivityService {

  constructor(private _http: HttpClient) { }

  public getSecurityActivities(): Observable<SecurityActivityModel[]> {
    return this._http.get<SecurityActivityModel[]>(environment.baseUrl + '/api/SecurityActivity');
  }
}
