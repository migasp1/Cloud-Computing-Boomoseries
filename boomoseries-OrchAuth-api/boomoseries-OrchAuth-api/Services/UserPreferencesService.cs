using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly HttpClient httpClient;
        //private static readonly string _userPreferencesBaseURL = Environment.GetEnvironmentVariable("PREFS_HOST");
        private static readonly string _userPreferencesBaseURL = "http://host.docker.internal:5024/UserPreferences/Favorites";
        //private static readonly string _userPreferencesBaseURL = "https://localhost:5025/UserPreferences/Favorites";
        private readonly ILogger<UserPreferencesService> logger;

        public UserPreferencesService(
            HttpClient httpClient,
            ILogger<UserPreferencesService> logger
            )
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<string> GetFavoriteWatchables(int id)
        {
            var url = _userPreferencesBaseURL + "?userId=" + id;
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> AddFavoriteWatchables(UserWatchablePreferenceDTO favWatchable)
        {
            JsonContent content = JsonContent.Create(favWatchable);
            var url = _userPreferencesBaseURL + "/Watchable";
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> AddFavoriteBook(UserBookPreferenceDTO bookModel)
        {
            JsonContent content = JsonContent.Create(bookModel);
            var url = _userPreferencesBaseURL + "/Book";
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
