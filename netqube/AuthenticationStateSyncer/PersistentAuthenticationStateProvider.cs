﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using netqube.AuthenticationStateSyncer.Models;

namespace netqube.AuthenticationStateSyncer;

public class PersistentAuthenticationStateProvider(PersistentComponentState persistentState) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> UnauthenticatedTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
        {
            return UnauthenticatedTask;
        }

        Claim[] claims = [
            new(ClaimTypes.NameIdentifier, userInfo.UserId),
            new(ClaimTypes.Name, userInfo.Name),
            new(ClaimTypes.Email, userInfo.Email)];

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: nameof(PersistentAuthenticationStateProvider)))));
    }
}