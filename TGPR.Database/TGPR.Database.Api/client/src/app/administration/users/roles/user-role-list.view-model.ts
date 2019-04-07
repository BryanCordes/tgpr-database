import {Injectable} from '@angular/core';
import {RoleModel} from '../../../_models/administration/users/role.model';
import {UserModel} from '../../../_models/administration/users/user.model';
import {UserRoleModel} from '../../../_models/administration/users/user-role.model';

export interface UserRoleDisplayModel {
  Role: RoleModel;
  UserRole: UserRoleModel;
}

@Injectable()
export class UserRoleListViewModel {

  public user: UserModel;
  public roles: RoleModel[];

  public userRoles: UserRoleDisplayModel[];

  constructor() { }

  public load() {
    this.userRoles = [];

    if (!this.user.Roles) {
      this.user.Roles = [];
    }

    for (const role of this.roles) {
      const display: UserRoleDisplayModel = {
        Role: role,
        UserRole: this.getRole(role)
      };

      this.userRoles.push(display);
    }
  }

  public add(model: UserRoleDisplayModel) {

    const userRole: UserRoleModel = {
      UserId: this.user.UserId,
      RoleId: model.Role.RoleId
    };

    model.UserRole = userRole;
    this.user.Roles.push(userRole);
  }

  public remove(model: UserRoleDisplayModel) {
    let roleId: string = model.Role.RoleId;

    model.UserRole = undefined;

    let index: number = this.user.Roles.findIndex(x => x.RoleId == roleId);
    if(index < 0){
      return;
    }

    this.user.Roles.splice(index, 1);
  }

  private getRole(role: RoleModel): UserRoleModel {
    if(!this.user
      || !this.user.Roles){
      return undefined;
    }

    // noinspection TsLint
    return this.user.Roles.find(x => x.RoleId == role.RoleId);
  }
}
