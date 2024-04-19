using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Volo.Abp.Account.LinkUsers;
using Volo.Abp.Account.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client.Authentication;

namespace Volo.Abp.Account.Pro.Public.Blazor.Shared.Pages.Account.LinkUsers;

public partial class LinkUsersModal
{
    protected Modal _modal;
    protected Modal _deleteConfirmationModal;
    protected Modal _newLinkUserConfirmationModal;

    [Inject]
    protected IOptions<AbpAccountLinkUserOptions> Options { get; set; }

    [Inject]
    protected IIdentityLinkUserAppService LinkUserAppService { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected IAbpAccessTokenProvider AccessTokenProvider { get; set; }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    protected ListResultDto<LinkUserDto> LinkUsers { get; set; }

    protected int PageSize { get; set; } = 5;

    protected string DeleteConfirmationMessage { get; set; }

    protected Guid? DeleteTenantId { get; set; }
    protected Guid DeleteUserId { get; set; }

    protected string PostAction { get; set; }
    protected string SourceLinkToken { get; set; }
    protected Guid? TargetLinkTenantId { get; set; }
    protected Guid TargetLinkUserId { get; set; }
    protected string ReturnUrl { get; set; }

    public LinkUsersModal()
    {
        LocalizationResource = typeof(AccountResource);
    }

    protected virtual async Task OpenModalAsync()
    {
        LinkUsers = await LinkUserAppService.GetAllListAsync();
        await InvokeAsync(_modal.Show);
    }

    protected virtual Task CloseModalAsync(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }

    protected virtual Task CloseDeleteConfirmationModalAsync(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }

    protected virtual Task CLoseNewLinkUserConfirmationModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }

    protected virtual async Task CloseSpecifyModalAsync(Modal modal)
    {
        await modal.Hide();
    }

    protected virtual async Task OpenDeleteConfirmationModalAsync(Guid? tenantId, Guid userId, string userName)
    {
        DeleteTenantId = tenantId;
        DeleteUserId = userId;
        DeleteConfirmationMessage = L["DeleteLinkAccountConfirmationMessage", userName];
        await _deleteConfirmationModal.Show();
    }

    protected virtual async Task NewLinkAccountAsync(bool isConfirmed)
    {
        if (!isConfirmed)
        {
            await _newLinkUserConfirmationModal.Show();
            return;
        }

        var linkToken = await LinkUserAppService.GenerateLinkTokenAsync();

        var loginUrl = Options.Value.LoginUrl?.EnsureEndsWith('/') ?? "/";
        var url =
            loginUrl +
            "Account/Login?handler=CreateLinkUser&" +
            "LinkUserId=" +
            CurrentUser.Id +
            "&LinkToken=" +
            UrlEncoder.Default.Encode(linkToken) +
            "&ReturnUrl=" + NavigationManager.Uri.EnsureEndsWith('/') + "Account/Challenge";

        if (CurrentTenant.Id != null)
        {
            url += "&LinkTenantId=" + CurrentTenant.Id;
        }

        NavigationManager.NavigateTo(url);
    }

    protected virtual async Task LoginAsThisAccountAsync(Guid? tenantId, Guid userId)
    {
        PostAction = "/Account/LinkLogin";
        ReturnUrl = NavigationManager.Uri;
        if (!Options.Value.LoginUrl.IsNullOrEmpty())
        {
            var accessToken = await AccessTokenProvider.GetTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                PostAction = Options.Value.LoginUrl.EnsureEndsWith('/') + "Account/LinkLogin";
                PostAction += "?access_token=" + accessToken;
                ReturnUrl = NavigationManager.Uri.EnsureEndsWith('/') + "Account/Challenge";
            }
        }

        TargetLinkTenantId = tenantId;
        TargetLinkUserId = userId;
        SourceLinkToken = await LinkUserAppService.GenerateLinkLoginTokenAsync();

        await InvokeAsync(StateHasChanged);

        await JSRuntime.InvokeVoidAsync("eval", "document.getElementById('linkUserLoginForm').submit()");
    }

    protected virtual async Task DeleteUsersAsync()
    {
        await _deleteConfirmationModal.Hide();

        await LinkUserAppService.UnlinkAsync(new UnLinkUserInput
        {
            TenantId = DeleteTenantId,
            UserId = DeleteUserId
        });

        DeleteTenantId = default;
        DeleteUserId = default;

        DeleteConfirmationMessage = string.Empty;

        LinkUsers = await LinkUserAppService.GetAllListAsync();
        await InvokeAsync(StateHasChanged);
    }
}
