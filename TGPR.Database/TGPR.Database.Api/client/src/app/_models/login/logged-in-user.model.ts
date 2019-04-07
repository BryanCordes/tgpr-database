import {SecurityActivityEnum} from './security-activity.enum';

export class LoggedInUserModel {
  id: string;
  email: string;
  activities: SecurityActivityEnum[];
}
