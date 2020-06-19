import {Routes} from '@angular/router';
import {DefaultLayoutComponent} from './layout/default.component';
import {AuthGuard} from './_guards/auth.guard';
import {LoginWrapperComponent} from './login/login-wrapper.component';
import {RoleAdministrationComponent} from './administration/roles/role-administration.component';
import {ActivityGuard} from './_guards/activity.guard';
import {EditableRolesResolver} from './_services/administration/roles/resolvers/editable-roles.resolver';
import {SecurityActivityEnum} from './_models/login/security-activity.enum';
import {SecurityActivitiesResolver} from './_services/administration/roles/resolvers/security-activities.resolver';
import {UserAdministrationComponent} from './administration/users/user-administration.component';
import {UserAdministrationUsersResolver} from './_services/administration/users/resolvers/user-administration-users.resolver';
import {CreateUserComponent} from './administration/users/create/create-user.component';
import {RolesResolver} from './_services/administration/roles/resolvers/roles.resolver';
import {EditUserComponent} from './administration/users/edit/edit-user.component';
import {UserAdministrationUserResolver} from './_services/administration/users/resolvers/user-administration-user.resolver';
import {ApplicationAdministrationComponent} from './administration/applications/application-administration.component';
import {ApplicationAdministrationTemplateResolver} from './_services/administration/applications/resolvers/application-administration-template.resolver';
import {ApplicationTypeResolver} from './_services/administration/applications/resolvers/application-type.resolver';
import {EditApplicationTemplateComponent} from './administration/applications/edit/edit-application-template.component';
import {ApplicationTemplateResolver} from './_services/administration/applications/resolvers/application-template.resolver';
import {ApplicationQuestionTypeResolver} from './_services/administration/applications/resolvers/application-question-type.resolver';
import {TestApplicationTemplateComponent} from './administration/applications/test/test-application-template.component';
import {CompanionApplicationsComponent} from './applications/companion/companion-applications.component';
import {CompanionApplicationsResolver} from './_services/applications/resolvers/companion-applications.resolver';

export const AppRoutes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'applications',
        redirectTo: 'applications/companion'
      }, {
        path: 'applications',
        children: [
          {
            path: 'companion',
            component: CompanionApplicationsComponent,
            resolve: {
              dataSourceResponse: CompanionApplicationsResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.CompanionApplicationRead
            }
          }
        ]
      }, {
        path: 'administration',
        redirectTo: 'administration/applications'
      }, {
        path: 'administration',
        children: [
          {
            path: 'roles',
            component: RoleAdministrationComponent,
            resolve: {
              roles: EditableRolesResolver,
              activities: SecurityActivitiesResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.UserRoleWrite
            }
          }, {
            path: 'users',
            component: UserAdministrationComponent,
            resolve : {
              dataSourceResponse: UserAdministrationUsersResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.UserWrite
            }
          }, {
            path: 'users/create',
            component: CreateUserComponent,
            resolve: {
              roles: RolesResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.UserWrite
            }
          }, {
            path: 'user/:id',
            component: EditUserComponent,
            resolve: {
              user: UserAdministrationUserResolver,
              roles: RolesResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.UserWrite
            }
          }, {
            path: 'applications',
            component: ApplicationAdministrationComponent,
            resolve: {
              dataSourceResponse: ApplicationAdministrationTemplateResolver,
              applicationTypes: ApplicationTypeResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.ApplicationTemplateWrite
            }
          }, {
            path: 'application/:id',
            pathMatch: 'full',
            component: EditApplicationTemplateComponent,
            resolve: {
              template: ApplicationTemplateResolver,
              questionTypes: ApplicationQuestionTypeResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.ApplicationTemplateWrite
            }
          }, {
            path: 'application/:id/test',
            pathMatch: 'full',
            component: TestApplicationTemplateComponent,
            resolve: {
              template: ApplicationTemplateResolver
            },
            canActivate: [ActivityGuard],
            data: {
              activity: SecurityActivityEnum.ApplicationTemplateWrite
            }
          }
        ]
      }
    ]
  }, {
    path: 'login',
    component: LoginWrapperComponent
  },
];
