import { NgModule } from '@angular/core';
import { ScheduleComponent } from './components/schedule.component';
import { ScheduleRoutingModule } from './schedule-routing.module';
import { CameraComponent } from '../../camera/camera.component';
import { WebcamModule } from 'ngx-webcam';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [


  ],
  imports: [
    ScheduleComponent,
    ScheduleRoutingModule,
    SharedModule

  ]
})
export class ScheduleModule { }
