import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebcamModule } from 'ngx-webcam'
import { CameraRoutingModule } from './camera-routing.module';
import { CameraComponent } from './camera.component';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    WebcamModule,
    CameraRoutingModule
  ]
})
export class CameraModule { }
