import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MaterialComponentsModule} from '../_ui/mat-components.module';
import {NiComponentsModule} from '../_ui/ni-components.module';
import {NgxMaskModule} from 'ngx-mask';
import {SweetAlert2Module} from '@sweetalert2/ngx-sweetalert2';
import {ApplicationReviewService} from '../_services/applications/application-review.service';
import {CompanionApplicationsResolver} from '../_services/applications/resolvers/companion-applications.resolver';
import {CompanionApplicationsComponent} from './companion/companion-applications.component';

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
    CompanionApplicationsComponent
  ],
  providers: [
    ApplicationReviewService,
    CompanionApplicationsResolver
  ]
})
export class ApplicationsModule { }
