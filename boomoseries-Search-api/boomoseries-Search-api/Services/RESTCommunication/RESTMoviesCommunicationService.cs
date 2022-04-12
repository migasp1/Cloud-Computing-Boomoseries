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
        private static readonly HttpClient httpClient = new();
        public RESTMoviesCommunicationService()
        {
        }

        public async Task<List<MovieDTO>> ObtainRandomMovies()
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[0] + "/random");

            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
            List<MovieDTO> movieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<MovieDTO> deserializedMovie = JsonConvert.DeserializeObject<List<MovieDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                movieDTOs.Add(item);
            }
            return movieDTOs;
        }

        public async Task<List<MovieDTO>> ObtainSepcificMovie(string movie_title)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[0] + "/" + movie_title);
            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
            List<MovieDTO> movieDTOs = new();
            var responseString = await response.Content.ReadAsStringAsync();
            List<MovieDTO> deserializedMovie = JsonConvert.DeserializeObject<List<MovieDTO>>(responseString);
            foreach (var item in deserializedMovie)
            {
                movieDTOs.Add(item);
            }
            return movieDTOs;
        }

        public async Task<List<MovieDTO>> GetMoviesByRating(double minRating)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[0] + "?minRating=" + minRating);
            //Wait for all the requests to finish
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;
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
