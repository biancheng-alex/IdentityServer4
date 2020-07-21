using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace oatuh2Client
{
    class Program
    {
        static void Main(string[] args)
        {
             clienta();
            Console.ReadLine();
        }


        private async static Task clienta()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            Console.WriteLine("Hello World!");
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client.api.service",
                ClientSecret = "clientsecret",
               GrantType= "client_credentials"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
           //var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/WeatherForecast");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.WriteLine(tokenResponse.Json);
        }
    }
}
