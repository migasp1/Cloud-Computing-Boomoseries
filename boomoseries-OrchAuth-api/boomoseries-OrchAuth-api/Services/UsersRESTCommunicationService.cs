using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UsersRESTCommunicationService : IUsersCommunicationService
    {
        private static readonly HttpClient _httpClient = new();

        public async Task<HttpResponseMessage> RegisterUser(RegisterModel registerModel)
        {
            JsonContent content = JsonContent.Create(registerModel);
            var response = await _httpClient.PostAsync(URLHelper._baseUsersMicroserviceURL, content);

            return response;
        }
    }
}
