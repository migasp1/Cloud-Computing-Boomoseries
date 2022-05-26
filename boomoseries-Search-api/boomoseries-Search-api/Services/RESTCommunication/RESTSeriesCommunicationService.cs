using boomoseries_Search_api.DTOs;
using boomoseries_Search_api.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services.RESTCommunication
{
    public class RESTSeriesCommunicationService : ICommunicationServiceSeries
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private readonly HttpClient httpClient;

        public RESTSeriesCommunicationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;   
        }

        public async Task<List<SerieDTO>> ObtainRandomSeries()
        {
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "/random");

            //Get the responses
            var response = request.Result;
            List<SerieDTO> serieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<SerieDTO> deserializedSerie = JsonConvert.DeserializeObject<List<SerieDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                serieDTOs.Add(item);
            }
            return serieDTOs;
        }

        public async Task<List<SerieDTO>> ObtainSepcificSeries(string series_title)
        {
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "/" + series_title);

            //Get the responses
            var response = request.Result;
            List<SerieDTO> serieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<SerieDTO> deserializedSerie = JsonConvert.DeserializeObject<List<SerieDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                serieDTOs.Add(item);
            }
            return serieDTOs;
        }

        public async Task<List<SerieDTO>> GetSeriesByRating(double minRating)
        {
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "?minRating=" + minRating);

            //Get the responses
            var response = request.Result;
            List<SerieDTO> serieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<SerieDTO> deserializedSerie = JsonConvert.DeserializeObject<List<SerieDTO>>(responseString);
            foreach (var item in deserializedSerie)
            {
                serieDTOs.Add(item);
            }
            return serieDTOs;
        }
    }


}
