using Microsoft.AspNetCore.Mvc;
using bomoseries_Series_api.Mapper;
using bomoseries_Series_api.DTOs;
using bomoseries_Series_api.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MoreLinq.Extensions;

namespace bomoseries_Series_api.Services.REST_Communication
{
    public class RESTCommunicationService : ICommunicationService
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        //private static readonly string IMDBBaseUrl = "http://host.docker.internal:5002/api/v1/IMDB";
        //private static readonly string IMDBBaseUrl = "https://localhost:5003/api/v1/IMDB/movies";
        private static readonly string IMDBBaseUrl = Environment.GetEnvironmentVariable("IMDB_HOST");
        private readonly HttpClient httpClient;

        public RESTCommunicationService(
            HttpClient httpClient
            )
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SerieDTO>> ObtainRandomSeries()
        {
            //Makes the requests to different microservices
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/random")).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            List<SerieDTO> SeriesDtos = new();
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
                    SerieDTO SeriesDto = SeriesEntityMapper.MapToDTO(item);
                    SeriesDtos.Add(SeriesDto);
                }
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();
            foreach (var serie in SeriesDtos)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + serie.Title);
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

            for (int i = 0; i < SeriesDtos.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(SeriesDtos[i].Title.ToLower()))
                    {
                        SeriesDtos[i].Director = uniqueImdb[j].Director;
                        SeriesDtos[i].Cast = uniqueImdb[j].Cast;
                    }
                }
            }

            var results = SeriesDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            return results;
        }

        public async Task<List<SerieDTO>> ObtainSepcificSeries(string seriesTitle)
        {
            //Makes the requests to different microservices
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/" + seriesTitle)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            List<SerieDTO> SeriesDtos = new();
            foreach (var response in responses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                SerieDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                SeriesDtos.Add(SeriesDto);
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();
            foreach (var serie in SeriesDtos)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + serie.Title);
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

            for (int i = 0; i < SeriesDtos.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(SeriesDtos[i].Title.ToLower()))
                    {
                        SeriesDtos[i].Director = uniqueImdb[j].Director;
                        SeriesDtos[i].Cast = uniqueImdb[j].Cast;
                    }
                }
            }

            var results = SeriesDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            return results;
        }

        public async Task<List<SerieDTO>> GetSeriesByRating(double min_rating)
        {
            List<SerieDTO> SeriesDtos = new();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "?min_rating=" + min_rating)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            
            
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
                    SerieDTO SeriesDto = SeriesEntityMapper.MapToDTO(item);
                    SeriesDtos.Add(SeriesDto);
                }
            }

            List<HttpResponseMessage> httpResponses = new List<HttpResponseMessage>();      
            foreach (var serie in SeriesDtos)
            {
                var requestTCC = await httpClient.GetAsync(IMDBBaseUrl + "/" + serie.Title);
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

            for (int i = 0; i < SeriesDtos.Count; i++)
            {
                for (int j = 0; j < uniqueImdb.Count; j++)
                {
                    if (uniqueImdb[j].Title.ToLower().Equals(SeriesDtos[i].Title.ToLower()))
                    {
                        SeriesDtos[i].Director = uniqueImdb[j].Director;  
                        SeriesDtos[i].Cast = uniqueImdb[j].Cast;  
                    }
                }
            }
            
            var results = SeriesDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();
            return results;
        }
    }
}
