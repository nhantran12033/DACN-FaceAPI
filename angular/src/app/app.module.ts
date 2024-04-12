import { FileManagementConfigModule } from '@volo/abp.ng.file-management/config';
import { CoreModule } from '@abp/ng.core';
import { GdprConfigModule } from '@volo/abp.ng.gdpr/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommercialUiConfigModule } from '@volo/abp.commercial.ng.ui/config';
import { AccountAdminConfigModule } from '@volo/abp.ng.account/admin/config';
import { AccountPublicConfigModule } from '@volo/abp.ng.account/public/config';
import { AuditLoggingConfigModule } from '@volo/abp.ng.audit-logging/config';
import { ChatConfigModule } from '@volo/abp.ng.chat/config';
import { FileManagementConfigModule } from '@volo/abp.ng.file-management/config';
import { IdentityConfigModule } from '@volo/abp.ng.identity/config';
import { LanguageManagementConfigModule } from '@volo/abp.ng.language-management/config';
import { registerLocale } from '@volo/abp.ng.language-management/locale';
import { SaasConfigModule } from '@volo/abp.ng.saas/config';
import { TextTemplateManagementConfigModule } from '@volo/abp.ng.text-template-management/config';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { OpeniddictproConfigModule } from '@volo/abp.ng.openiddictpro/config';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { AbpOAuthModule } from '@abp/ng.oauth';
import { HttpErrorComponent, ThemeLeptonXModule } from '@volosoft/abp.ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/layouts';
import { AccountLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/account';
import { DEPARTMENTS_DEPARTMENT_ROUTE_PROVIDER } from './departments/department/providers/department-route.provider';
import { SALARIES_SALARY_ROUTE_PROVIDER } from './salaries/salary/providers/salary-route.provider';
import { SCHEDULES_SCHEDULE_ROUTE_PROVIDER } from './schedules/schedule/providers/schedule-route.provider';
import { TIMESHEETS_TIMESHEET_ROUTE_PROVIDER } from './timesheets/timesheet/providers/timesheet-route.provider';
import { STAFFS_STAFF_ROUTE_PROVIDER } from './staffs/staff/providers/staff-route.provider';
@NgModule({
  declarations: [AppComponent],
  imports: [
    FileManagementConfigModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    ThemeSharedModule.forRoot({
      httpErrorConfig: {
        errorScreen: {
          component: HttpErrorComponent,
          forWhichErrors: [401, 403, 404, 500],
          hideCloseIcon: true,
        },
      },
    }),
    AccountAdminConfigModule.forRoot(),
    AccountPublicConfigModule.forRoot(),
    FileManagementConfigModule.forRoot(),
    ChatConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    LanguageManagementConfigModule.forRoot(),
    SaasConfigModule.forRoot(),
    AuditLoggingConfigModule.forRoot(),
    OpeniddictproConfigModule.forRoot(),
    TextTemplateManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),

    CommercialUiConfigModule.forRoot(),
    FeatureManagementModule.forRoot(),
    GdprConfigModule.forRoot({
      privacyPolicyUrl: 'gdpr-cookie-consent/privacy',
      cookiePolicyUrl: 'gdpr-cookie-consent/cookie',
    }),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    AccountLayoutModule.forRoot(),
  ],
  providers: [
    APP_ROUTE_PROVIDER,
    DEPARTMENTS_DEPARTMENT_ROUTE_PROVIDER,
    SALARIES_SALARY_ROUTE_PROVIDER,
    SCHEDULES_SCHEDULE_ROUTE_PROVIDER,
    TIMESHEETS_TIMESHEET_ROUTE_PROVIDER,
    STAFFS_STAFF_ROUTE_PROVIDER,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
