import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';

import { ScheduleFormatViewService } from '../services/schedule-format.service';
import { ScheduleFormatDetailViewService } from '../services/schedule-format-detail.service';
import { ScheduleFormatDetailComponent } from './schedule-format-detail.component';
import { AbstractScheduleFormatComponent } from './schedule-format.abstract.component';

@Component({
  selector: 'app-schedule-format',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    ScheduleFormatDetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    PageModule,
  ],
  providers: [
    ListService,
    ScheduleFormatViewService,
    ScheduleFormatDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './schedule-format.component.html',
  styles: [],
})
export class ScheduleFormatComponent extends AbstractScheduleFormatComponent {}
