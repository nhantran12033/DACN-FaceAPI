﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Volo.Abp.Account.Public.Web.Pages.Account;

public class LogoutModel : AccountPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string ReturnUrlHash { get; set; }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        if (CurrentUser.IsAuthenticated)
        {
            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = IdentitySecurityLogActionConsts.Logout
            });
        }

        await SignInManager.SignOutAsync();
        await HttpContext.SignOutAsync(ConfirmUserModel.ConfirmUserScheme);
        await HttpContext.SignOutAsync(ChangePasswordModel.ChangePasswordScheme);

        if (ReturnUrl != null)
        {
            return RedirectSafely(ReturnUrl, ReturnUrlHash);
        }

        return RedirectToPage("~/Account/Login");
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }
}
