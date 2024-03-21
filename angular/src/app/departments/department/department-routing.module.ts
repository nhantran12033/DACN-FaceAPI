import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { DepartmentComponent } from './components/department.component';

export const routes: Routes = [
  {
    path: '',
    component: DepartmentComponent,
    canActivate: mapToCanActivate([AuthGuard, PermissionGuard]),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DepartmentRoutingModule {}
