using IdentityServer4.Models;
using System.Collections.Generic;

namespace Biod.AuthServer.Core
{
    public class Config
    {
        // clients that are allowed to access resources from the Auth server 
        public static IEnumerable<Client> GetClients()
        {
            // client credentials, list of clients
            return new List<Client>
                {
                    new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
 
                        // Client secrets
                        ClientSecrets =
                        {
                            new Secret("bluedotsecret".Sha256())
                        },
                        AllowedScopes = { "BluedotApi" }
                    },
                };
        }

        // API that are allowed to access the Auth server
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
                {
                new ApiResource("BluedotApi", "BlueDot API")
                };
        }
    }
}