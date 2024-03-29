import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {NiAlertComponent} from './ni-alert/ni-alert.component';
import {NiBadgeComponent} from './ni-badge/ni-badge.component';
import {GradientDirective} from './directives/gradient/gradient.directive';
import {BgDirective} from './directives/bg/bg.directive';
import {ColorDirective} from './directives/color/color.directive';
import {NiVTimelineComponent} from './ni-v-timeline/ni-v-timeline.component';
import {NiFileComponent} from './ni-file/ni-file.component';
import {NiChatComponent} from './ni-chat/ni-chat.component';
import {NiCardComponent} from './ni-card/ni-card.component';
import {NiButtonComponent} from './ni-button/ni-button.component';
import {NiBreadcrumbComponent} from './ni-breadcrumb/ni-breadcrumb.component';
import {AutoGrowDirective} from './directives/auto-grow/auto-grow.directive';
import {RouterModule} from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  declarations: [
    NiAlertComponent,
    NiBadgeComponent,
    NiBreadcrumbComponent,
    NiButtonComponent,
    NiCardComponent,
    NiChatComponent,
    NiFileComponent,
    NiVTimelineComponent,
    ColorDirective,
    BgDirective,
    GradientDirective,
    AutoGrowDirective
  ],
  exports: [
    NiAlertComponent,
    NiBadgeComponent,
    NiBreadcrumbComponent,
    NiButtonComponent,
    NiCardComponent,
    NiChatComponent,
    NiFileComponent,
    NiVTimelineComponent,
    ColorDirective,
    BgDirective,
    GradientDirective
  ]
})
export class NiComponentsModule { }
