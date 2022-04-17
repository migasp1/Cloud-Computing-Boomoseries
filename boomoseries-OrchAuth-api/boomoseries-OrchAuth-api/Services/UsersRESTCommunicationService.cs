using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UsersRESTCommunicationService : IUsersCommunicationService
    {
        private static readonly HttpClient _httpClient = new();

        public async Task<HttpResponseMessage> AuthenticateUser(AuthenticateModel authenticateModel)
        {
            JsonContent content = JsonContent.Create(authenticateModel);
            var response = await _httpClient.PostAsync(URLHelper._baseUsersMicroserviceURL + "/authenticate", content);

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(RegisterModel registerModel)
        {
            JsonContent content = JsonContent.Create(registerModel);
            var response = await _httpClient.PostAsync(URLHelper._baseUsersMicroserviceURL + "/register", content);

            return response;
        }

        public async Task<User> GetUserById(int id)
        {
            var request = _httpClient.GetAsync(URLHelper._baseUsersMicroserviceURL + "/" + id);
            var response = request.Result;
            var responseString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseString);

            return user;
        }
    }
}
