﻿using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private static readonly HttpClient httpClient = new();

        public async Task<string> GetFavoriteWatchables(int id)
        {
            var response = await httpClient.GetAsync(URLHelper._baseUserPreferencesMicroserviceURL + "?userId=" + id);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
