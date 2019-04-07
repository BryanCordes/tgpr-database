import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginModel} from '../../_models/login/login.model';
import {Observable} from 'rxjs/internal/Observable';
import {environment} from '../../../environments/environment';
import { map } from 'rxjs/operators';
import {Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {SecurityActivityEnum} from '../../_models/login/security-activity.enum';

@Injectable()
export class AuthenticationService {

  constructor(private _httpClient: HttpClient, private _router: Router) { }

  public login(loginModel: LoginModel): Observable<any> {
    return this._httpClient.post<any>(environment.baseUrl + '/api/Authentication/login', loginModel)
      .pipe(
        map(jwt => {
          if (!jwt.Error) {
            this.setToken(jwt);
            return '';
          }

          return jwt.Error;
        })
      );
  }

  public logout(returnUrl: string = undefined) {
    localStorage.removeItem('token');
    localStorage.removeItem('auth_token');

    this._router.navigate(['login']);

    // if(returnUrl
    //    && returnUrl.length > 0) {
    //   this._router.navigate(['', { queryParams: { returnUrl: returnUrl }}]);
    // }
    // else {
    //   this._router.navigate(['']);
    // }
  }

  public isExpired() {
    const jwt: string = this.getAuthToken();

    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(jwt);

    return isExpired;
  }

  public getAuthToken(): string {
    const token = localStorage.getItem('auth_token');
    if (!token) {
      return null;
    }

    return token;
  }

  private setToken(data: any) {
    if (!data || !data.auth_token) {
      return;
    }

    const token: string = JSON.stringify(data);

    localStorage.setItem('token', token);
    localStorage.setItem('auth_token', data.auth_token);
  }
}
