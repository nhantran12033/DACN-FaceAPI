using FaceAPI.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FaceAPI;

[DependsOn(
    typeof(FaceAPIEntityFrameworkCoreTestModule)
    )]
public class FaceAPIDomainTestModule : AbpModule
{

}
