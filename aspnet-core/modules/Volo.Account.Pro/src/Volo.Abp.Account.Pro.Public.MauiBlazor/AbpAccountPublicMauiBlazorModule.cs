using Microsoft.AspNetCore.Components.Authorization;
using Volo.Abp.Account.Pro.Public.MauiBlazor.OAuth;
using Volo.Abp.AspNetCore.Components.MauiBlazor.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Volo.Abp.Account.Pro.Public.MauiBlazor;

[DependsOn(
    typeof(AbpAspNetCoreComponentsMauiBlazorThemingModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAccountPublicHttpApiClientModule)
)]
public class AbpAccountPublicMauiBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorizationCore();
        context.Services.AddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAccountPublicMauiBlazorModule).Assembly);
        });
    }
}
