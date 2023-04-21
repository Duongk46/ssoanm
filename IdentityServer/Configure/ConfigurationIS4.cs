
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Configure
{
    public static class ConfigurationIS4
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "WebApi",
                    DisplayName = "Web Api",
                    Scopes = new List<string> { "apiRead", "apiWrite" },
                    ApiSecrets = new List<Secret> { new Secret("webapi_xuanhuongvanhungnguoiban".Sha256())}
                }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id_mvc_1",
                    ClientSecrets = { new Secret("client_xuanhuongvanhungnguoiban_1".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "WebApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    RedirectUris = { "https://localhost:44365/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44365/Home" },
                    AllowedCorsOrigins = { "https://localhost:44365" },
                    AllowOfflineAccess = true, // cho phép refresh ở project ngoài
                    RequireConsent = false,
                    AccessTokenLifetime = 30, // thời gian access token hoạt động
                    UpdateAccessTokenClaimsOnRefresh = true, // update claim khi refresh

                },
                 new Client
                {
                    ClientId = "client_id_mvc_2",
                    ClientSecrets = { new Secret("client_xuanhuongvanhungnguoiban_2".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "WebApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    RedirectUris = { "https://localhost:44346/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44346/Home" },
                    AllowedCorsOrigins = { "https://localhost:44346" },
                    AllowOfflineAccess = true, // cho phép refresh ở project ngoài
                    RequireConsent = false,
                    AccessTokenLifetime = 30, // thời gian access token hoạt động
                    UpdateAccessTokenClaimsOnRefresh = true, // update claim khi refresh

                }
            };
    }
}
