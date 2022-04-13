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
        private static readonly HttpClient httpClient = new();

        public RESTSeriesCommunicationService()
        {
        }

        public async Task<List<SerieDTO>> ObtainRandomSeries()
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "/random");

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
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
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "/" + series_title);

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
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
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[1] + "?minRating=" + minRating);

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
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
