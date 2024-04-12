using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.FileManagement;
using Volo.Chat;

namespace FaceAPI;

[DependsOn(
    typeof(FaceAPIDomainModule),
    typeof(FaceAPIApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(SaasHostApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(AbpGdprApplicationModule),
    typeof(TextTemplateManagementApplicationModule)
    )]
[DependsOn(typeof(FileManagementApplicationModule))]
    [DependsOn(typeof(ChatApplicationModule))]
    public class FaceAPIApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient("Face", client =>
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiY2NjZWZkMDItNTMwNi00ODY3LWFjN2YtZjdiYmVjNDVhOTcyIiwidHlwZSI6ImFwaV90b2tlbiJ9.qFPls3BE8ADKEcKKqyo7hjwbc14gxcca4iTC0Zu2jxE");
        });
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FaceAPIApplicationModule>();
        });
    }
}
