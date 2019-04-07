import {RoleSecurityActivityModel} from './role-security-activity.model';

export class RoleModel {
  RoleId?: string;
  Name: string;

  SecurityActivities: RoleSecurityActivityModel[];
}
