﻿using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSFTIdentity.Services
{
    public static class ResourceConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resourceapi", "Resource API")
                {
                    Scopes = {new Scope("api.read")}
                }
            };
        }

        public static IEnumerable<Client> GetClients(string devHost = "")
        {
            return new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "js_test_client",
                    ClientName = "Javascript Test Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "api.read" },
                    RedirectUris = {$"http://{devHost}/test-client/callback.html"}, // test client runs on same host
                    AllowedCorsOrigins = {$"http://{devHost}" }, // test client runs on same host
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                 new Client {
                    RequireConsent = false,
                    ClientId = "angular_spa",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "api.read" },
                    RedirectUris = {"http://localhost:4200/auth-callback"}, // test client runs on same host,
                    PostLogoutRedirectUris = new List<string> {"http://localhost:4200/"},
                    AllowedCorsOrigins = {"http://localhost:4200" }, // test client runs on same host
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                }
            };
        }
    }
}
