using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.AuthorityDelegation;
using Volo.Abp.Account.Localization;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Account.Pro.Public.Blazor.Shared.Pages.Account.AuthorityDelegation;

public partial class AuthorityDelegationModal
{
    protected Modal _modal;

    protected Modal _delegateNewUserModal;
    
    protected Modal _deleteConfirmationModal;
    
    [Inject]
    protected IOptions<AbpAccountAuthorityDelegationOptions> Options { get; set; }
    
    [Inject]
    protected IIdentityUserDelegationAppService UserDelegationAppService { get; set; }
    
    [Inject]
    protected NavigationManager NavigationManager { get; set; }
    
    protected ListResultDto<UserDelegationDto> DelegatedUsers { get; set; }
    
    protected ListResultDto<UserDelegationDto> MyDelegatedUsers { get; set; }
    
    protected ListResultDto<UserLookupDto> Users { get; set; }

    protected int PageSize { get; set; } = 5;
    
    protected string SelectedTab = "delegated-users";

    protected string DeleteConfirmationMessage { get; set; }
    
    protected Guid DeleteId { get; set; }
    
    protected DelegateNewUserInput DelegateNewUserInput { get; set; } = new();
    
    protected IReadOnlyList<DateTime?> DelegateNewUserDataRange;

    public AuthorityDelegationModal()
    {
        LocalizationResource = typeof(AccountResource);
    }

    protected virtual async Task OnSelectedTabChangedAsync(string name)
    {
        SelectedTab = name;
        
        if(SelectedTab == "delegated-users")
        {
            await GetDelegatedUsersAsync();
        }
        else
        {
            await GetMyDelegatedUsersAsync();
        }
    }
    
    protected virtual async Task GetDelegatedUsersAsync()
    {
        DelegatedUsers = await UserDelegationAppService.GetDelegatedUsersAsync();
    }

    protected virtual async Task GetMyDelegatedUsersAsync()
    {
        MyDelegatedUsers = await UserDelegationAppService.GetMyDelegatedUsersAsync();
    }

    protected virtual async Task OpenDeleteConfirmationModalAsync(Guid id, string userName)
    {
        DeleteId = id;
        DeleteConfirmationMessage = L["DeleteUserDelegationConfirmationMessage", userName];
        await _deleteConfirmationModal.Show();
    }
    
    protected virtual async Task DeleteDelegatedUsersAsync()
    {
        await CloseConfirmationModal();
        await UserDelegationAppService.DeleteDelegationAsync(DeleteId);
        await GetDelegatedUsersAsync();
        DeleteId = default;
        DeleteConfirmationMessage = string.Empty;
    }

    protected virtual async Task DelegateNewUserAsync()
    {
        try
        {
            if (DelegateNewUserInput.TargetUserId == default)
            {
                await Message.Error(@L["AuthorityDelegation:PleaseSelectUser"]);
                return;
            }

            if (DelegateNewUserDataRange.Min() == null || DelegateNewUserDataRange.Max() == null)
            {
                await Message.Error(@L["AuthorityDelegation:PleaseSelectDelegationDateRange"]);
                return;
            }
            
            DelegateNewUserInput.StartTime = DelegateNewUserDataRange.Min().Value;
            DelegateNewUserInput.EndTime = DelegateNewUserDataRange.Max().Value;
            await UserDelegationAppService.DelegateNewUserAsync(DelegateNewUserInput);
            await GetDelegatedUsersAsync();
            await CloseDelegateNewUserModal();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    
    protected virtual async Task GetUserLookupAsync(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs )
    {
        Users = await UserDelegationAppService.GetUserLookupAsync(
            new GetUserLookupInput 
            {
                Filter = autocompleteReadDataEventArgs.SearchValue 
            });
    }
    
    protected virtual async Task OpenModalAsync()
    {
        await GetDelegatedUsersAsync();
        await InvokeAsync(_modal.Show);
    }
    
    protected virtual async Task OpenDelegateNewUserModalAsync()
    {
        DelegateNewUserInput = new DelegateNewUserInput();
        DelegateNewUserDataRange = new List<DateTime?> {null, null};
        await InvokeAsync(_delegateNewUserModal.Show);
    }

    protected virtual string GetStatus(UserDelegationDto content)
    {
        var status = "";
        var curr = DateTime.Now;
        if (content.StartTime > curr) 
        {
            status = "Future";
        }
        else if (curr > content.EndTime)
        {
            status = "Expired";
        } 
        else if (content.StartTime < curr && curr < content.EndTime)
        {
            status = "Active";
        }
        return status;
    }

    protected virtual string GetStatusBadge(string status)
    {
        var badge = status switch {
            "Future" => "warning",
            "Expired" => "danger",
            "Active" => "success",
            _ => ""
        };
        return badge;
    }
    
    protected virtual void DelegatedImpersonate(UserDelegationDto context)
    {
        NavigationManager.NavigateTo("Account/DelegatedImpersonate?UserDelegationId=" + context.Id, forceLoad:true);
    }

    protected virtual async Task CloseModalAsync()
    {
        await InvokeAsync(_modal.Hide);
    }

    protected virtual async Task CloseDelegateNewUserModal()
    {
        await InvokeAsync(_delegateNewUserModal.Hide);
    }

    protected virtual async Task CloseConfirmationModal()
    {
        await InvokeAsync(_deleteConfirmationModal.Hide);
    }
    
    protected virtual Task ClosingModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }
    
    protected virtual Task ClosingDeleteConfirmationModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.EscapeClosing
                           || eventArgs.CloseReason == CloseReason.FocusLostClosing;

        return Task.CompletedTask;
    }
}