import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { ScheduleFormatComponent } from './components/schedule-format.component';

export const routes: Routes = [
  {
    path: '',
    component: ScheduleFormatComponent,
    canActivate: mapToCanActivate([AuthGuard, PermissionGuard]),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ScheduleFormatRoutingModule {}
