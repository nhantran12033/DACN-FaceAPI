﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Account.Pro.Public.MauiBlazor.OAuth;

public class PasswordFlowExternalAuthService : ExternalAuthServiceBase
{
    public const string HttpClientName = "PasswordFlowExternalAuthService";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OAuthConfigOptions _oAuthConfigOptions;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICurrentTenantAccessor _currentTenantAccessor;

    public PasswordFlowExternalAuthService(
        IHttpClientFactory httpClientFactory,
        IOptions<OAuthConfigOptions> options,
        ICurrentTenant currentTenant,
        ICurrentTenantAccessor currentTenantAccessor,
        IAccessTokenStore accessTokenStore) : base(accessTokenStore)
    {
        _httpClientFactory = httpClientFactory;
        _oAuthConfigOptions = options.Value;
        _currentTenant = currentTenant;
        _currentTenantAccessor = currentTenantAccessor;
    }

    public async override Task<LoginResult> LoginAsync(LoginInput loginInput)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var discoveryDocument = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _oAuthConfigOptions.Authority,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = _oAuthConfigOptions.RequireHttpsMetadata
            }
        });

        var passwordTokenRequest = new PasswordTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = _oAuthConfigOptions.ClientId,
            ClientSecret = _oAuthConfigOptions.ClientSecret,
            UserName = loginInput.UserNameOrEmailAddress,
            Password = loginInput.Password,
            Scope = _oAuthConfigOptions.Scope
        };

        if (_currentTenant.IsAvailable)
        {
            passwordTokenRequest.Headers.Add("__tenant", _currentTenant.Id.ToString());
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(passwordTokenRequest);

        if (tokenResponse.IsError)
        {
            return LoginResult.Failed(tokenResponse.Error, tokenResponse.ErrorDescription);
        }

        await AccessTokenStore.SetAccessTokenAsync(tokenResponse.AccessToken);
        CurrentUser = new ClaimsPrincipal(new ClaimsIdentity(new JwtSecurityTokenHandler().ReadJwtToken(tokenResponse.AccessToken).Claims, AuthenticationType));
        TriggerUserChanged();

        return LoginResult.Success();
    }

    public async override Task SignOutAsync()
    {
        await AccessTokenStore.SetAccessTokenAsync(null);

        CurrentUser = new ClaimsPrincipal(new ClaimsIdentity());
        _currentTenantAccessor.Current = new BasicTenantInfo(null);
        TriggerUserChanged();
    }
}
