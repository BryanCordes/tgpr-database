import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {ApplicationModel} from '../../../_models/applications/application.model';

@Injectable()
export class ApplicationService {

  constructor(private http: HttpClient) { }

  create(application: ApplicationModel) {
    return this.http.post<ApplicationModel>(environment.baseUrl + "/api/Application/create", application);
  }
}
