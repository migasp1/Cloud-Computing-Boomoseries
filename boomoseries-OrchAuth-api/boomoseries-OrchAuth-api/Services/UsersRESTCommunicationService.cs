using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UsersRESTCommunicationService : IUsersCommunicationService
    {
        private static readonly string _usersBaseURL = "http://host.docker.internal:5020/Users";
        //private static readonly string _usersBaseURL = "https://localhost:5021/Users";
        //private static readonly string _usersBaseURL = Environment.GetEnvironmentVariable("USERS_HOST");
        private readonly HttpClient httpClient;
        private readonly ILogger<UsersRESTCommunicationService> logger;

        public UsersRESTCommunicationService(
            HttpClient httpClient,
            ILogger<UsersRESTCommunicationService> logger
            )
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<HttpResponseMessage> AuthenticateUser(AuthenticateModel authenticateModel)
        {
            JsonContent content = JsonContent.Create(authenticateModel);
            var url = _usersBaseURL + "/authenticate";
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.PostAsync(url, content);

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(RegisterModel registerModel)
        {
            JsonContent content = JsonContent.Create(registerModel);
            var url = _usersBaseURL + "/register";
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.PostAsync(url, content);

            return response;
        }

        public async Task<User> GetUserById(int id)
        {
            var url = _usersBaseURL + "/" + id;
            logger.LogInformation("Invoking url {Url}", url);
            var request = httpClient.GetAsync(url);
            var response = request.Result;
            var responseString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseString);

            return user;
        }
    }
}
