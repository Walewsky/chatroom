using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace ChatRoom.IdentityProvider
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ChatRoomApi",
                    ClientName = "API for ChatRoom",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())},
                    AllowedScopes = new List<string> {"ChatRoom"},
                },
                new Client
                {
                    ClientId = "ChatRoomWeb",
                    ClientName = "Web Application for ChatRoom",
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string> {"http://localhost:4200/"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ChatRoom"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                }
            };
        }
    }

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List< IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "ChatRoom",
                    DisplayName = "Chat Application",
                    Description = "Allow the application to access Chat Application on your behalf",
                    Scopes = new List<string> {"ChatRoom"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("ChatRoom", "Access to the ChatRoom"),
            };
        }
    }

    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "testuser1",
                    Password = "Password@123",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "testuser1@outlook.com")
                    }
                },
                new TestUser {
                    SubjectId = "49B798E1-BC91-4C26-BB60-F5C7D9BA126F",
                    Username = "testuser2",
                    Password = "Password@123",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "testuser2@outlook.com")
                    }
                }
            };
        }
    }
}
