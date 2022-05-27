using boomoseries_OrchAuth_api.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class SearchRESTCommunicationServiceWatchables : ISearchCommunicationServiceWatchables
    {
        //private static readonly string microserviceBaseURL = "http://host.docker.internal:5018/api/v1/Search";
        private static readonly string microserviceBaseURL = "https://localhost:5019/api/v1/Search";
        //private static readonly string microserviceBaseURL = Environment.GetEnvironmentVariable("SEARCH_HOST");
        private readonly HttpClient httpClient;

        public SearchRESTCommunicationServiceWatchables(
            HttpClient httpClient
            )
        {
            this.httpClient = httpClient;
        }

        public async Task<object> GetWatchblesByRating(string type, double minRating)
        {
            var response = await httpClient.GetAsync(microserviceBaseURL + "?type=" + type + "&minRating=" + minRating);
            if (((int)response.StatusCode == 400))
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<WatchableDTO> watchableDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<WatchableDTO> deserializedSerie = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                watchableDTOs.Add(item);
            }
            return watchableDTOs;

        }

        public async Task<object> ObtainSepcificWatchables(string type, string watchable_title)
        {
            var response = await httpClient.GetAsync(microserviceBaseURL + "/" + watchable_title + "?type=" + type);
            if (((int)response.StatusCode == 400))
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<WatchableDTO> watchableDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<WatchableDTO> deserializedSerie = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                watchableDTOs.Add(item);
            }
            return watchableDTOs;
        }

        public async Task<object> ObtainRandomWatchable(string type)
        {
            var response = await httpClient.GetAsync(microserviceBaseURL + "/random" + "?type=" + type);
            if (((int)response.StatusCode == 400))
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<WatchableDTO> watchableDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<WatchableDTO> deserializedMovie = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                watchableDTOs.Add(item);
            }
            return watchableDTOs;
        }
    }
}
