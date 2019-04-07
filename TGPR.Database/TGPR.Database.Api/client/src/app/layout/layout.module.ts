import {NgModule} from '@angular/core';
import {TopNavbarComponent} from './navbars/top/top-navbar.component';
import {DefaultLayoutComponent} from './default.component';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {SideNavbarComponent} from './navbars/side/side-navbar.component';
import {SideMenuComponent} from './navbars/side/menu/side-menu.component';
import {SideMenuLogoComponent} from './navbars/side/logo/side-menu-logo.component';
import {MaterialComponentsModule} from '../_ui/mat-components.module';
import {TimeoutComponent} from './timeout/timeout.component';
import {TimeoutDialogComponent} from './timeout/timeout.dialog';
import {TimeoutService} from './timeout/timeout.service';
import {NgIdleKeepaliveModule} from '@ng-idle/keepalive';
import {SweetAlert2Module} from '@sweetalert2/ngx-sweetalert2';
import {UserService} from '../_services/authentication/user.service';

@NgModule({
  imports: [
    HttpClientModule,
    CommonModule,
    RouterModule,
    MaterialComponentsModule,
    SweetAlert2Module.forRoot({
      customClass: 'modal-content',
      confirmButtonClass: 'btn btn-primary',
      cancelButtonClass: 'btn',
      heightAuto: false
    }),
    NgIdleKeepaliveModule.forRoot()
  ],
  declarations: [
    DefaultLayoutComponent,
    TopNavbarComponent,
    SideNavbarComponent,
    SideMenuComponent,
    SideMenuLogoComponent,
    TimeoutComponent,
    TimeoutDialogComponent
  ],
  entryComponents: [
    TimeoutDialogComponent
  ],
  providers: [
    TimeoutService,
    UserService
  ]
})

export class LayoutModule { }

