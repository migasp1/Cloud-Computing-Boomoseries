using boomoseries_OrchAuth_api.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class SearchRESTCommunicationServiceWatchables : ISearchCommunicationServiceWatchables
    {
        private static readonly string microserviceBaseURL = "http://host.docker.internal:5018/api/v1/Search";
        //private static readonly string microserviceBaseURL = "https://localhost:5019/api/v1/Search";
        //private static readonly string microserviceBaseURL = Environment.GetEnvironmentVariable("SEARCH_HOST");
        private readonly HttpClient httpClient;
        private readonly ILogger<SearchRESTCommunicationServiceWatchables> logger;

        public SearchRESTCommunicationServiceWatchables(ILogger<SearchRESTCommunicationServiceWatchables> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }

 

        public async Task<object> GetWatchblesByRating(string type, double minRating)
        {
            var url = microserviceBaseURL + "?type=" + type + "&minRating=" + minRating;
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
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
            var url = microserviceBaseURL + "/" + watchable_title + "?type=" + type;
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
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
            var url = microserviceBaseURL + "/random" + "?type=" + type;
            logger.LogInformation("Invoking url {Url}", url);
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
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
