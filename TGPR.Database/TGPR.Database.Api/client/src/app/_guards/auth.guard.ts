import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {AuthenticationService} from '../_services/authentication/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private _router: Router, private _authService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(!localStorage.getItem('token')){
      this._router.navigate(['login']);

      return false;
    }

    if (this._authService.isExpired()) {
      // not logged in so redirect to login Page
      this._authService.logout(state.url);

      return false;
    }

    return true;
  }
}
