using boomoseries_OrchAuth_api.DTOs;
using boomoseries_OrchAuth_api.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class SearchRESTCommunicationServiceWatchables : ISearchCommunicationServiceWatchables
    {
        private static readonly string microserviceBaseURL = URLHelper._baseSearchMicroserviceURL;
        private static readonly HttpClient httpClient = new();

        public SearchRESTCommunicationServiceWatchables()
        {
        }

        public async Task<List<WatchableDTO>> GetWatchblesByRating(string type, double minRating)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microserviceBaseURL + "?type=" + type + "&minRating=" + minRating);

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
            List<WatchableDTO> watchableDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<WatchableDTO> deserializedSerie = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                watchableDTOs.Add(item);
            }
            return watchableDTOs;

        }

        public async Task<List<WatchableDTO>> ObtainSepcificWatchables(string type, string watchable_title)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microserviceBaseURL + "/" + watchable_title + "?type=" + type);

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
            List<WatchableDTO> watchableDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<WatchableDTO> deserializedSerie = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                watchableDTOs.Add(item);
            }
            return watchableDTOs;
        }

        public async Task<List<WatchableDTO>> ObtainRandomWatchable(string type)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microserviceBaseURL + "/random" + "?type=" + type);

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
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
