using FaceAPI.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FaceAPI.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FaceAPIController : AbpControllerBase
{
    protected FaceAPIController()
    {
        LocalizationResource = typeof(FaceAPIResource);
    }
}
