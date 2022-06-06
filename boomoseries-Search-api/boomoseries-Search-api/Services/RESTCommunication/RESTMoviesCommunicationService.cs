using boomoseries_Search_api.DTOs;
using boomoseries_Search_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_Search_api.Services.RESTCommunication
{
    public class RESTMoviesCommunicationService : ICommunicationServiceMovies
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private readonly HttpClient httpClient;
        public RESTMoviesCommunicationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<object> ObtainRandomMovies()
        {
            var request = httpClient.GetAsync(microservicesBaseURL[0] + "/random");

            //Get the responses
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<MovieDTO> movieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<MovieDTO> deserializedMovie = JsonConvert.DeserializeObject<List<MovieDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                movieDTOs.Add(item);
            }
            return movieDTOs;
        }

        public async Task<object> ObtainSepcificMovie(string movie_title)
        {
            var request = httpClient.GetAsync(microservicesBaseURL[0] + "/" + movie_title);

            //Get the responses
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<MovieDTO> movieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<MovieDTO> deserializedMovie = JsonConvert.DeserializeObject<List<MovieDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                movieDTOs.Add(item);
            }
            return movieDTOs;
        }

        public async Task<object> GetMoviesByRating(double minRating)
        {

            var request = httpClient.GetAsync(microservicesBaseURL[0] + "?minRating=" + minRating);

            //Get the responses
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            List<MovieDTO> movieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<MovieDTO> deserializedMovie = JsonConvert.DeserializeObject<List<MovieDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                movieDTOs.Add(item);
            }
            return movieDTOs;
        }

    }
}
