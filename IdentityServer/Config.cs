// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using Microservice.Shared.Constants;

namespace Microservice.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource(ApiResourceConstants.CatalogApi){Scopes={ApiResourceConstants.CatalogApiScope}},
            new ApiResource(ApiResourceConstants.PhotoStockApi){Scopes={ApiResourceConstants.PhotoApiScope}},
            new ApiResource(ApiResourceConstants.BasketApi){Scopes={ApiResourceConstants.BasketApiScope}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource()
                       {
                           Name = "Role",
                           DisplayName ="Roles",
                           Description = "Kullanıcı Rolleri",
                           UserClaims = new []
                           {
                               "roles"
                           }
                       }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(ApiResourceConstants.CatalogApiScope,"Catalog Api Tam Yetki"),
                new ApiScope(ApiResourceConstants.PhotoApiScope,"PhotoStock Api Tam Yetki"),
                new ApiScope(ApiResourceConstants.BasketApiScope,"Basket Api Tam Yetki"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "UiClient",
                    ClientName = "Ui Client Test",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("mysecret".Sha256()) },

                    AllowedScopes = { ApiResourceConstants.CatalogApiScope, ApiResourceConstants.PhotoApiScope, IdentityServerConstants.LocalApi.ScopeName }
                },

                new Client
                {
                    ClientId = "UiClient_WClaims",
                    ClientName = "Ui Client with Claims",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // for refresh token
                    ClientSecrets = { new Secret("mysecret".Sha256()) },

                    AllowedScopes = { IdentityServerConstants.StandardScopes.Email, 
                                      IdentityServerConstants.StandardScopes.OpenId,
                                      IdentityServerConstants.StandardScopes.Profile,
                                      IdentityServerConstants.StandardScopes.OfflineAccess, // for refresh token
                                      IdentityServerConstants.LocalApi.ScopeName,
                                      ApiResourceConstants.BasketApiScope,
                                      "roles",
                    },
                    AccessTokenLifetime =1*60*60, // default
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(30)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}