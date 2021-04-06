using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBookIdentity.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //Key lien quan thong tin cua User
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        //Create API resource
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //Mo ta mot so API ma minh muon truy cap hay cap quyen su dung
                new ApiResource("api", "Main API Resource")
            };
        }

        //Create API client
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                //Create client
                new Client
                {
                    //Set Client ID
                    ClientId = "client",
                    //AllowedGrantTypes: loai Token
                    //ResourceOwnerPassword: can co userName, password thi moi duoc cap Token
                    //ClientCredentials: Chi can co thong tin client(khong can user) la lay duoc Token
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api", "openid", "profile", "email" }
                },
            };
        }
    }
}
