import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
  NgbNavModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';

import { ScheduleDetailViewService } from '../services/schedule-detail.service';
import { ScheduleDetailDetailViewService } from '../services/schedule-detail-detail.service';
import { ScheduleDetailDetailComponent } from './schedule-detail-detail.component';
import { AbstractScheduleDetailComponent } from './schedule-detail.abstract.component';

@Component({
  selector: 'app-schedule-detail',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    ScheduleDetailDetailComponent,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    NgbNavModule,
    PageModule,
  ],
  providers: [
    ListService,
    ScheduleDetailViewService,
    ScheduleDetailDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './schedule-detail.component.html',
  styles: [],
})
export class ScheduleDetailComponent extends AbstractScheduleDetailComponent {}
