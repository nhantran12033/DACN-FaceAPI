using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace FaceAPI;

[Dependency(ReplaceServices = true)]
public class FaceAPIBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "FaceAPI";
}
