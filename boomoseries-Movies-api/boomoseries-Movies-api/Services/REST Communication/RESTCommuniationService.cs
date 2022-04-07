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

        public async Task<string> ObtainMovies()
        {

            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);

            foreach (var r in responses)
            {
                // Extract the message body
                r.EnsureSuccessStatusCode();
                string s = await r.Content.ReadAsStringAsync();
                if (s.Contains("Netflix"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no netflix");
                }
                if (s.Contains("Amazon"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Amazon Prime");
                }
                if (s.Contains("Disney"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Disney+");
                }
                if (s.Contains("IMDB"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no IMDB");
                }
            }

            return "Success!";
        }

        public async Task<string> ObtainSepcificMovie(string movieTitle)
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

            foreach (var r in responses)
            {
                // Extract the message body
                r.EnsureSuccessStatusCode();
                string s = await r.Content.ReadAsStringAsync();
                if (s.Contains("Netflix"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no netflix");
                }
                if (s.Contains("Amazon"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Amazon Prime");
                }
                if (s.Contains("Disney"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Disney+");
                }
                if (s.Contains("IMDB"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no IMDB");
                }
            }

            return "Success!";
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
