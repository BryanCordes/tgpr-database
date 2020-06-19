import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MaterialComponentsModule} from '../_ui/mat-components.module';
import {NiComponentsModule} from '../_ui/ni-components.module';
import {RoleService} from '../_services/administration/roles/role.service';
import {RoleSecurityActivityService} from '../_services/administration/roles/role-security-activity.service';
import {RoleAdministrationComponent} from './roles/role-administration.component';
import {RoleActivityAdministrationComponent} from './roles/activities/role-activity-administration.component';
import {CreateRoleDialogComponent} from './roles/create/create-role-dialog.component';
import {SecurityActivityService} from '../_services/administration/roles/security-activity.service';
import {SecurityActivitiesResolver} from '../_services/administration/roles/resolvers/security-activities.resolver';
import {EditableRolesResolver} from '../_services/administration/roles/resolvers/editable-roles.resolver';
import {SweetAlert2Module} from '@sweetalert2/ngx-sweetalert2';
import {EditRoleDialogComponent} from './roles/edit/edit-role-dialog.component';
import {UserAdministrationUsersResolver} from '../_services/administration/users/resolvers/user-administration-users.resolver';
import {UserAdministrationService} from '../_services/administration/users/user-administration.service';
import {UserAdministrationComponent} from './users/user-administration.component';
import {CreateUserComponent} from './users/create/create-user.component';
import {RolesResolver} from '../_services/administration/roles/resolvers/roles.resolver';
import {UserRoleService} from '../_services/administration/users/user-role.service';
import {UserRoleListComponent} from './users/roles/user-role-list.component';
import {UserAdministrationUserResolver} from '../_services/administration/users/resolvers/user-administration-user.resolver';
import {EditUserComponent} from './users/edit/edit-user.component';
import {NgxMaskModule} from 'ngx-mask';
import {ApplicationTemplateService} from '../_services/administration/applications/application-template.service';
import {ApplicationQuestionTypeService} from '../_services/administration/applications/application-question-type.service';
import {ApplicationOptionService} from '../_services/administration/applications/application-option.service';
import {ApplicationQuestionService} from '../_services/administration/applications/application-question.service';
import {ApplicationCategoryService} from '../_services/administration/applications/application-category.service';
import {ApplicationAdministrationTemplateResolver} from '../_services/administration/applications/resolvers/application-administration-template.resolver';
import {ApplicationTypeResolver} from '../_services/administration/applications/resolvers/application-type.resolver';
import {ApplicationTemplateResolver} from '../_services/administration/applications/resolvers/application-template.resolver';
import {ApplicationQuestionTypeResolver} from '../_services/administration/applications/resolvers/application-question-type.resolver';
import {ApplicationAdministrationComponent} from './applications/application-administration.component';
import {CreateApplicationTemplateDialogComponent} from './applications/create/create-application-template-dialog.component';
import {RenameApplicationTemplateDialogComponent} from './applications/rename/rename-application-template-dialog.component';
import {EditApplicationTemplateComponent} from './applications/edit/edit-application-template.component';
import {ApplicationAdministrationCategoriesComponent} from './applications/categories/application-administration-categories.component';
import {CreateApplicationCategoryDialogComponent} from './applications/categories/create/create-application-category-dialog.component';
import {EditApplicationCategoryDialogComponent} from './applications/categories/edit/edit-application-category-dialog.component';
import {ApplicationAdministrationQuestionsComponent} from './applications/questions/application-administration-questions.component';
import {CreateApplicationQuestionQuestionDialogComponent} from './applications/questions/create/create-application-question-dialog.component';
import {EditApplicationQuestionDialogComponent} from './applications/questions/edit/edit-application-question-dialog.component';
import {ApplicationAdministrationOptionsComponent} from './applications/options/application-administration-options.component';
import {CreateApplicationOptionDialogComponent} from './applications/options/create/create-application-option-dialog.component';
import {EditApplicationOptionDialogComponent} from './applications/options/edit/edit-application-option-dialog.component';
import {SharedService} from '../layout/shared.service';
import {BrowserModule} from '@angular/platform-browser';
import {TestApplicationTemplateComponent} from './applications/test/test-application-template.component';
import {TestApplicationTemplateQuestionComponent} from './applications/test/questions/test-application-template-question.component';
import {QuestionTypeTextComponent} from './applications/test/questions/types/text/question-type-text.component';
import {QuestionTypeSingleComponent} from './applications/test/questions/types/single/question-type-single.component';
import {TestApplicationTemplateQuestionsComponent} from './applications/test/question-groups/test-application-template-questions.component';
import {QuestionTypeMultiComponent} from './applications/test/questions/types/multiple/question-type-multi.component';
import {QuestionTypeDropdownComponent} from './applications/test/questions/types/dropdown/question-type-dropdown.component';
import {QuestionTypeInfoComponent} from './applications/test/questions/types/information/question-type-info.component';
import {ApplicationService} from '../_services/administration/applications/application.service';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialComponentsModule,
    NiComponentsModule,
    NgxMaskModule.forRoot(),
    SweetAlert2Module.forRoot({
      customClass: 'modal-content',
      confirmButtonClass: 'btn btn-primary',
      cancelButtonClass: 'btn',
      heightAuto: false
    }),
  ],
  declarations: [
    UserAdministrationComponent,
    CreateUserComponent,
    EditUserComponent,
    UserRoleListComponent,
    RoleAdministrationComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    RoleActivityAdministrationComponent,
    ApplicationAdministrationComponent,
    CreateApplicationTemplateDialogComponent,
    RenameApplicationTemplateDialogComponent,
    EditApplicationTemplateComponent,
    ApplicationAdministrationCategoriesComponent,
    CreateApplicationCategoryDialogComponent,
    EditApplicationCategoryDialogComponent,
    ApplicationAdministrationQuestionsComponent,
    CreateApplicationQuestionQuestionDialogComponent,
    EditApplicationQuestionDialogComponent,
    ApplicationAdministrationOptionsComponent,
    CreateApplicationOptionDialogComponent,
    EditApplicationOptionDialogComponent,
    TestApplicationTemplateComponent,
    TestApplicationTemplateQuestionsComponent,
    TestApplicationTemplateQuestionComponent,
    QuestionTypeTextComponent,
    QuestionTypeSingleComponent,
    QuestionTypeMultiComponent,
    QuestionTypeDropdownComponent,
    QuestionTypeInfoComponent
  ],
  entryComponents: [
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    CreateApplicationTemplateDialogComponent,
    RenameApplicationTemplateDialogComponent,
    CreateApplicationCategoryDialogComponent,
    EditApplicationCategoryDialogComponent,
    CreateApplicationQuestionQuestionDialogComponent,
    EditApplicationQuestionDialogComponent,
    CreateApplicationOptionDialogComponent,
    EditApplicationOptionDialogComponent
  ],
  providers: [
    SharedService,
    UserAdministrationService,
    UserRoleService,
    RoleService,
    RoleSecurityActivityService,
    SecurityActivityService,
    RolesResolver,
    EditableRolesResolver,
    SecurityActivitiesResolver,
    UserAdministrationUsersResolver,
    UserAdministrationUserResolver,
    ApplicationTemplateService,
    ApplicationCategoryService,
    ApplicationQuestionService,
    ApplicationOptionService,
    ApplicationQuestionTypeService,
    ApplicationAdministrationTemplateResolver,
    ApplicationTypeResolver,
    ApplicationTemplateResolver,
    ApplicationQuestionTypeResolver,
    ApplicationService
  ]
})

export class AdministrationModule { }
