using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Helpers;
using boomoseries_Movies_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Services.REST_Communication
{
    public class RESTCommuniationService : ICommunicationService
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly HttpClient httpClient = new();

        public RESTCommuniationService()
        {
        }

        public async Task<List<MovieDTO>> ObtainRandomMovies()
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/random")).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            List<MovieDTO> movieDTOs = new();
            foreach (var response in responses)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Contains("Netflix"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("Amazon"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("Disney"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("IMDB"))
                {
                    //WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    //MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    //movieDTOs.Add(movieDto);
                    Debug.WriteLine("APARECEU");
                }
            }
            return movieDTOs;
        }

        public async Task<List<MovieDTO>> ObtainSepcificMovie(string movieTitle)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/" + movieTitle)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            List<MovieDTO> movieDTOs = new();
            foreach (var response in responses)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Contains("Netflix"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("Amazon"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("Disney"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    movieDTOs.Add(movieDto);
                }
                else if (responseString.Contains("IMDB"))
                {
                    //WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    //MovieDTO movieDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                    //movieDTOs.Add(movieDto);
                    Debug.WriteLine("APARECEU");
                }
            }
            return movieDTOs;
        }

        [HttpGet("movies")]
        public async Task<List<MovieDTO>> GetMoviesByRating(double min_rating)
        {
            List<MovieDTO> movieDtos = new();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "?min_rating=" + min_rating)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            foreach (var response in responses)
            {
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                List<WatchableDTO> deserializedWatchable = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
                foreach (var item in deserializedWatchable)
                {
                    MovieDTO movieDto = MovieEntityMapper.MapToDTO(item);
                    movieDtos.Add(movieDto);
                }
            }

            var results = movieDtos.GroupBy(m => m.Platform).SelectMany(movies => movies).ToList();

            return results;
        }
    }
}
