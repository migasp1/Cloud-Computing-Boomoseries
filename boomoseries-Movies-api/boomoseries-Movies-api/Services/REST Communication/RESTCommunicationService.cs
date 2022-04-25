using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Helpers;
using boomoseries_Movies_api.Mapper;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Services.REST_Communication
{
    public class RESTCommunicationService : ICommunicationService
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly string IMDBBaseUrl = Environment.GetEnvironmentVariable("IMDB_HOST");
        private static readonly HttpClient httpClient = new();

        public RESTCommunicationService()
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
            List<MovieDTO> movieDtos = new();
            foreach (var response in responses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                List<WatchableDTO> deserializedWatchable = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
                foreach (var item in deserializedWatchable)
                {
                    MovieDTO MoviesDto = MovieEntityMapper.MapToDTO(item);
                    movieDtos.Add(MoviesDto);
                }
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();
            foreach (var movie in movieDtos)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + movie.Title);
                httpResponses.Add(requestTCC);
            }

            List<IMDBDTO> imdbDTOs = new();

            foreach (var response in httpResponses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                IMDBWatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<IMDBWatchableDTO>(responseString);
                IMDBDTO imdbTCC = IMDBEntityMapper.MapToDTO(deserializedWatchable);
                imdbDTOs.Add(imdbTCC);
            }

            List<IMDBDTO> uniqueImdb = imdbDTOs.DistinctBy(x => x.Title).ToList();

            for (int i = 0; i < movieDtos.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(movieDtos[i].Title.ToLower()))
                    {
                        movieDtos[i].Director = uniqueImdb[j].Director;
                        movieDtos[i].Cast = uniqueImdb[j].Cast;
                    }
                }
            }

            var results = movieDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            return results;
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
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                MovieDTO MoviesDto = MovieEntityMapper.MapToDTO(deserializedWatchable);
                movieDTOs.Add(MoviesDto);
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();
            foreach (var movie in movieDTOs)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + movie.Title);
                httpResponses.Add(requestTCC);
            }

            List<IMDBDTO> imdbDTOs = new();

            foreach (var response in httpResponses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                IMDBWatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<IMDBWatchableDTO>(responseString);
                IMDBDTO imdbTCC = IMDBEntityMapper.MapToDTO(deserializedWatchable);
                imdbDTOs.Add(imdbTCC);
            }

            List<IMDBDTO> uniqueImdb = imdbDTOs.DistinctBy(x => x.Title).ToList();

            for (int i = 0; i < movieDTOs.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(movieDTOs[i].Title.ToLower()))
                    {
                        movieDTOs[i].Director = uniqueImdb[j].Director;
                        movieDTOs[i].Cast = uniqueImdb[j].Cast;
                    }
                }
            }

            var results = movieDTOs.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            return results;
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
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                List<WatchableDTO> deserializedWatchable = JsonConvert.DeserializeObject<List<WatchableDTO>>(responseString);
                foreach (var item in deserializedWatchable)
                {
                    MovieDTO MoviesDto = MovieEntityMapper.MapToDTO(item);
                    movieDtos.Add(MoviesDto);
                }
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();
            foreach (var movie in movieDtos)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + movie.Title);
                httpResponses.Add(requestTCC);
            }

            List<IMDBDTO> imdbDTOs = new();

            foreach (var response in httpResponses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                IMDBWatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<IMDBWatchableDTO>(responseString);
                IMDBDTO imdbTCC = IMDBEntityMapper.MapToDTO(deserializedWatchable);
                imdbDTOs.Add(imdbTCC);
            }

            List<IMDBDTO> uniqueImdb = imdbDTOs.DistinctBy(x => x.Title).ToList();

            for (int i = 0; i < movieDtos.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(movieDtos[i].Title.ToLower()))
                    {
                        movieDtos[i].Director = uniqueImdb[j].Director;
                        movieDtos[i].Cast = uniqueImdb[j].Cast;
                    }
                }
            }

            var results = movieDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            return results;
        }
    }
}
