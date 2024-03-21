using Volo.Abp.Modularity;

namespace FaceAPI;

[DependsOn(
    typeof(FaceAPIApplicationModule),
    typeof(FaceAPIDomainTestModule)
    )]
public class FaceAPIApplicationTestModule : AbpModule
{

}
