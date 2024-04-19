import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbCollapseModule, NgbDatepickerModule, NgbDropdownModule, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';
import { WebcamModule, WebcamComponent } from 'ngx-webcam'; // Ensure WebcamComponent is imported if used

import { ScheduleDetailComponent } from './schedule-detail.component';
import { ScheduleViewService } from '../services/schedule.service';
import { ScheduleDetailViewService } from '../services/schedule-detail.service';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ListService } from '@abp/ng.core';
import { AbstractScheduleComponent } from './schedule.abstract.component';
import { CameraComponent } from '../../../camera/camera.component';

@Component({
  selector: 'app-schedule',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,

  imports: [
    CoreModule,
    ThemeSharedModule,
    ScheduleDetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    PageModule,
  ],
  providers: [
    ListService,
    ScheduleViewService,
    ScheduleDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],

  templateUrl: './schedule.component.html',
  styles: []
})
export class ScheduleComponent extends AbstractScheduleComponent { }
