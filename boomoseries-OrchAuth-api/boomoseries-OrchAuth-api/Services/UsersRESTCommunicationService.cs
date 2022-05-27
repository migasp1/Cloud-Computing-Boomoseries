using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UsersRESTCommunicationService : IUsersCommunicationService
    {
        //private static readonly string _usersBaseURL = "http://host.docker.internal:5020/Users";
        private static readonly string _usersBaseURL = "https://localhost:5021/Users";
        //private static readonly string _usersBaseURL = Environment.GetEnvironmentVariable("USERS_HOST");
        private readonly HttpClient httpClient;

        public UsersRESTCommunicationService(
            HttpClient httpClient
            )
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> AuthenticateUser(AuthenticateModel authenticateModel)
        {
            JsonContent content = JsonContent.Create(authenticateModel);
            var response = await httpClient.PostAsync(_usersBaseURL + "/authenticate", content);

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(RegisterModel registerModel)
        {
            JsonContent content = JsonContent.Create(registerModel);
            var response = await httpClient.PostAsync(_usersBaseURL + "/register", content);

            return response;
        }

        public async Task<User> GetUserById(int id)
        {
            var request = httpClient.GetAsync(_usersBaseURL + "/" + id);
            var response = request.Result;
            var responseString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseString);

            return user;
        }
    }
}
