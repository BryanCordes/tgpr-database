import {Injectable} from '@angular/core';
import {RoleSecurityActivityModel} from '../../../_models/administration/users/role-security-activity.model';
import {RoleModel} from '../../../_models/administration/users/role.model';
import {SecurityActivityModel} from '../../../_models/administration/users/security-activity.model';
import {RoleSecurityActivityService} from '../../../_services/administration/roles/role-security-activity.service';

export interface RoleActivityDisplayModel {
  Activity: SecurityActivityModel;
  RoleActivity: RoleSecurityActivityModel;
}

@Injectable()
export class RoleActivityAdministrationViewModel {

  public role: RoleModel;
  public activities: SecurityActivityModel[];

  public roleActivities: RoleActivityDisplayModel[];

  constructor(private _service: RoleSecurityActivityService) { }

  public load() {
    this.roleActivities = [];

    for (const activity of this.activities) {
      const display: RoleActivityDisplayModel = {
        Activity: activity,
        RoleActivity: this.getActivity(activity)
      };

      this.roleActivities.push(display);
    }
  }

  public add(model: RoleActivityDisplayModel) {
    this._service.add(this.role.RoleId, model.Activity.SecurityActivityId)
      .subscribe(roleSecurityActivity => {
        model.RoleActivity = roleSecurityActivity;
      });
  }

  public remove(model: RoleActivityDisplayModel) {
    this._service.remove(model.RoleActivity.RoleSecurityActivityId)
      .subscribe(() => {
        model.RoleActivity = undefined;
      });
  }

  private getActivity(activity: SecurityActivityModel): RoleSecurityActivityModel {
    if(!this.role
       || !this.role.SecurityActivities){
      return undefined;
    }

    // noinspection TsLint
    return this.role.SecurityActivities.find(x => x.SecurityActivityId == activity.SecurityActivityId);
  }
}
