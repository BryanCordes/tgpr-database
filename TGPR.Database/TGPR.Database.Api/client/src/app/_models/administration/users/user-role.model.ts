import {RoleModel} from './role.model';

export class UserRoleModel {
  UserRoleId?: string;
  UserId: string;
  RoleId: string;

  Role?: RoleModel;
}
