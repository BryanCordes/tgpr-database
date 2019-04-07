import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {UserService} from '../_services/authentication/user.service';
import {AuthGuard} from './auth.guard';

@Injectable()
export class ActivityGuard implements CanActivate {

  constructor(private _router: Router, private _authGuard: AuthGuard, private _userService: UserService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    const auth: boolean = this._authGuard.canActivate(route, state);
    if (!auth) {
      return false;
    }

    const securityActivity = route.data.activity;
    const hasActivity = this._userService.hasActivity(securityActivity);
    if (!hasActivity) {
      this._router.navigate(['']);

      return false;
    }

    return true;
  }
}
