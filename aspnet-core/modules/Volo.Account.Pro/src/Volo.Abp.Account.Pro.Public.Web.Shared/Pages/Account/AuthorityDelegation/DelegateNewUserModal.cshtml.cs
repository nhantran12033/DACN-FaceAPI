using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Volo.Abp.Account.Pro.Public.Web.Shared.Pages.Account.AuthorityDelegation;

public class DelegateNewUserModal : AbpPageModel
{
    protected IIdentityUserDelegationAppService IdentityUserDelegationAppService { get; }

    public DelegateNewUserModal(IIdentityUserDelegationAppService identityUserDelegationAppService)
    {
        IdentityUserDelegationAppService = identityUserDelegationAppService;
        LocalizationResourceType = typeof(AccountResource);
    }

    public virtual Task<IActionResult> OnGetAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }
}