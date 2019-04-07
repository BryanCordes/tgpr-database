import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {LayoutModule} from './layout/layout.module';
import {LoginModule} from './login/login.module';
import {RouterModule} from '@angular/router';
import {AppRoutes} from './app.routing';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AuthGuard} from './_guards/auth.guard';
import {AdministrationModule} from './administration/administration.module';
import {ActivityGuard} from './_guards/activity.guard';
import {JwtInterceptor} from './_services/authentication/jwt.interceptor';
import {HTTP_INTERCEPTORS} from '@angular/common/http';

@NgModule({
  imports: [
    BrowserModule,
    LayoutModule,
    LoginModule,
    AdministrationModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes),
  ],
  declarations: [
    AppComponent
  ],
  providers: [
    AuthGuard,
    ActivityGuard,
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
