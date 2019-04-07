import {UserRoleModel} from './user-role.model';

export class UserSummaryModel {
  UserId: string;
  Email: string;
  PhoneNumber: string;
  FirstName: string;
  LastName: string;
  Address: string;
  CreatedOn: Date;
  LastLoginOn?: Date;
  InactiveOn?: Date;

  Roles: UserRoleModel[];
}
