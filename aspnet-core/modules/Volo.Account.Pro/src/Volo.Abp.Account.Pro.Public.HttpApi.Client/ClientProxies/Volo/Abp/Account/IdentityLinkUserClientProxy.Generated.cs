// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Modeling;

// ReSharper disable once CheckNamespace
namespace Volo.Abp.Account;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IIdentityLinkUserAppService), typeof(IdentityLinkUserClientProxy))]
public partial class IdentityLinkUserClientProxy : ClientProxyBase<IIdentityLinkUserAppService>, IIdentityLinkUserAppService
{
    public virtual async Task LinkAsync(LinkUserInput input)
    {
        await RequestAsync(nameof(LinkAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LinkUserInput), input }
        });
    }

    public virtual async Task UnlinkAsync(UnLinkUserInput input)
    {
        await RequestAsync(nameof(UnlinkAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UnLinkUserInput), input }
        });
    }

    public virtual async Task<bool> IsLinkedAsync(IsLinkedInput input)
    {
        return await RequestAsync<bool>(nameof(IsLinkedAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IsLinkedInput), input }
        });
    }

    public virtual async Task<string> GenerateLinkTokenAsync()
    {
        return await RequestAsync<string>(nameof(GenerateLinkTokenAsync));
    }

    public virtual async Task<bool> VerifyLinkTokenAsync(VerifyLinkTokenInput input)
    {
        return await RequestAsync<bool>(nameof(VerifyLinkTokenAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VerifyLinkTokenInput), input }
        });
    }

    public virtual async Task<string> GenerateLinkLoginTokenAsync()
    {
        return await RequestAsync<string>(nameof(GenerateLinkLoginTokenAsync));
    }

    public virtual async Task<bool> VerifyLinkLoginTokenAsync(VerifyLinkLoginTokenInput input)
    {
        return await RequestAsync<bool>(nameof(VerifyLinkLoginTokenAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VerifyLinkLoginTokenInput), input }
        });
    }

    public virtual async Task<ListResultDto<LinkUserDto>> GetAllListAsync()
    {
        return await RequestAsync<ListResultDto<LinkUserDto>>(nameof(GetAllListAsync));
    }
}
