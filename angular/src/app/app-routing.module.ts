import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard, PermissionGuard],
  },
  {
    path: 'account',
    loadChildren: () =>
      import('@volo/abp.ng.account/public').then(m => m.AccountPublicModule.forLazy()),
  },
  {
    path: 'gdpr',
    loadChildren: () => import('@volo/abp.ng.gdpr').then(m => m.GdprModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@volo/abp.ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'language-management',
    loadChildren: () =>
      import('@volo/abp.ng.language-management').then(m => m.LanguageManagementModule.forLazy()),
  },
  {
    path: 'saas',
    loadChildren: () => import('@volo/abp.ng.saas').then(m => m.SaasModule.forLazy()),
  },
  {
    path: 'audit-logs',
    loadChildren: () =>
      import('@volo/abp.ng.audit-logging').then(m => m.AuditLoggingModule.forLazy()),
  },
  {
    path: 'openiddict',
    loadChildren: () =>
      import('@volo/abp.ng.openiddictpro').then(m => m.OpeniddictproModule.forLazy()),
  },
  {
    path: 'text-template-management',
    loadChildren: () =>
      import('@volo/abp.ng.text-template-management').then(m =>
        m.TextTemplateManagementModule.forLazy()
      ),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  {
    path: 'gdpr-cookie-consent',
    loadChildren: () =>
      import('./gdpr-cookie-consent/gdpr-cookie-consent.module').then(
        m => m.GdprCookieConsentModule
      ),
  },
  {
    path: 'departments',
    loadChildren: () =>
      import('./departments/department/department.module').then(m => m.DepartmentModule),
  },
  {
    path: 'salaries',
    loadChildren: () => import('./salaries/salary/salary.module').then(m => m.SalaryModule),
  },
  {
    path: 'schedules',
    loadChildren: () => import('./schedules/schedule/schedule.module').then(m => m.ScheduleModule),
  },
  {
    path: 'timesheets',
    loadChildren: () =>
      import('./timesheets/timesheet/timesheet.module').then(m => m.TimesheetModule),
  },
  {
    path: 'staffs',
    loadChildren: () => import('./staffs/staff/staff.module').then(m => m.StaffModule),
  },
  {
    path: 'schedule-details',
    loadChildren: () =>
      import('./schedule-details/schedule-detail/schedule-detail.module').then(
        m => m.ScheduleDetailModule
      ),
  },
  {
    path: 'schedule-formats',
    loadChildren: () =>
      import('./schedule-formats/schedule-format/schedule-format.module').then(
        m => m.ScheduleFormatModule
      ),
  },
  { path: 'camera', loadChildren: () => import('./camera/camera.module').then(m => m.CameraModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
