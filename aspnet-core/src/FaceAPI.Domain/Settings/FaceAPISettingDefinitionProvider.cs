using Volo.Abp.Settings;

namespace FaceAPI.Settings;

public class FaceAPISettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FaceAPISettings.MySetting1));
    }
}
