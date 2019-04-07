import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {NiComponentsModule} from '../_ui/ni-components.module';
import {LoginComponent} from './login.component';
import {AuthenticationService} from '../_services/authentication/authentication.service';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MaterialComponentsModule} from '../_ui/mat-components.module';
import {LoginWrapperComponent} from './login-wrapper.component';
import {UserService} from '../_services/authentication/user.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialComponentsModule,
    NiComponentsModule
  ],
  declarations: [
    LoginComponent,
    LoginWrapperComponent
  ],
  providers: [
    AuthenticationService,
    UserService
  ]
})

export class LoginModule { }

