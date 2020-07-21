using System;

using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace oauth2
{
    public class Config
    {
        /// 这个ApiResource参数就是我们Api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetSoluction()
        {
            return new[]
            {
               new ApiResource("api1", "MY API"),
                               new ApiResource("clientsecret", "CAS Client Service"),
                new ApiResource("productsecret", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    AllowedScopes = {"api1"}
                },
                 new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientsecret" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientsecret", "productsecret" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientsecret", "productsecret" }
                },
                        new Client
        {
            ClientId = "mvc",
            ClientName = "MVC Client",
            AllowedGrantTypes = GrantTypes.Implicit,

            // 登录成功回调处理地址，处理回调返回的数据
            RedirectUris = { "http://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            }
        }

            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
