import {UserRoleModel} from './user-role.model';

export class UserModel {
  UserId?: string;
  Email: string;
  PasswordHash: string;
  PhoneNumber: string;
  FirstName: string;
  LastName: string;
  Address: string;
  CreatedOn?: Date;
  LastLoginOn?: Date;
  InactiveOn?: Date;

  Roles?: UserRoleModel[];
}
