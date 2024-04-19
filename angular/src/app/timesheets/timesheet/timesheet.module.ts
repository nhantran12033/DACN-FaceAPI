import { NgModule } from '@angular/core';
import { TimesheetComponent } from './components/timesheet.component';
import { TimesheetRoutingModule } from './timesheet-routing.module';
import { WebcamModule } from 'ngx-webcam';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [TimesheetComponent, TimesheetRoutingModule, WebcamModule, SharedModule],
})
export class TimesheetModule {}
