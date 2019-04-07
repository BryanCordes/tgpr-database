import {Injectable} from '@angular/core';
import {AuthenticationService} from './authentication.service';
import {LoggedInUserModel} from '../../_models/login/logged-in-user.model';
import {JwtHelperService} from '@auth0/angular-jwt';
import {SecurityActivityEnum} from '../../_models/login/security-activity.enum';

@Injectable()
export class UserService {

  constructor(private _authService: AuthenticationService) { }

  public getUser(): LoggedInUserModel {
    const jwt = this._authService.getAuthToken();

    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(jwt);

    const user: LoggedInUserModel = {
      id: decodedToken.id,
      email: decodedToken.sub,
      activities: decodedToken.activity
        .map(x => parseInt(x, 10))
    };

    return user;
  }

  public hasActivity(securityActivity: SecurityActivityEnum): boolean {
    const user: LoggedInUserModel = this.getUser();

    const activity = user.activities
      .find(x => x === securityActivity);

    return !!activity;
  }

  public hasAnyActivity(securityActivities: SecurityActivityEnum[]): boolean {

    // there are no security activities required for this route
    if(!securityActivities) {
      return true;
    }

    for(const securityActivity of securityActivities) {
      const hasActivity = this.hasActivity(securityActivity);
      if(hasActivity) {
        return true;
      }
    }

    return false;
  }
}
