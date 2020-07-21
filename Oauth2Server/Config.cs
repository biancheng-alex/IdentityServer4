using System;

using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Collections.Generic;
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
               new ApiResource("api1", "MY API")
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
