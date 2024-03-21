using FaceAPI.Localization;
using Volo.Abp.Application.Services;

namespace FaceAPI;

/* Inherit your application services from this class.
 */
public abstract class FaceAPIAppService : ApplicationService
{
    protected FaceAPIAppService()
    {
        LocalizationResource = typeof(FaceAPIResource);
    }
}
