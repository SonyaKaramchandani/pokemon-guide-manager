using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.Core.Client.Test
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //discover endpoints from metadata
            //Discovering the endpoints by calling the auth server hosted on 5000 port
            //var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            var disco = await DiscoveryClient.GetAsync("https://web1demo.bluedot.global/Biod.AuthServer.Core/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //request token
            //Once we have the endpoints, we are creating Client Token which contains the endpoints, ClinetId(in our case it is “client”) and client secret
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "bluedotsecret");
            //Once the TokenClient is created, we will request the token from the auth server
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("BluedotApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            //call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://localhost:5001/identity");
            var response = await client.GetAsync("http://web1dev.ad.bluedot.global/Biod.Zebra.Api.Core/Identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadLine();
        }
    }
}
