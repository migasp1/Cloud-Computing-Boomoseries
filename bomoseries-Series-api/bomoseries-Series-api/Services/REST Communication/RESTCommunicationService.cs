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


namespace bomoseries_Series_api.Services.REST_Communication
{
    public class RESTCommunicationService : ICommunicationService
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly HttpClient httpClient = new();

        public RESTCommunicationService()
        {
        }

        public async Task<List<SeriesDTO>> ObtainRandomSeries()
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
            List<SeriesDTO> SeriesDTOs = new();
            foreach (var response in responses)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Contains("Netflix"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("Amazon"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("Disney"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("IMDB"))
                {
                    //WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    //SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    //SeriesDTOs.Add(SeriesDto);
                    Debug.WriteLine("APARECEU");
                }
            }
            return SeriesDTOs;
        }

        public async Task<List<SeriesDTO>> ObtainSepcificSeries(string seriesTitle)
        {
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/" + seriesTitle)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);
            List<SeriesDTO> SeriesDTOs = new();
            foreach (var response in responses)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Contains("Netflix"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("Amazon"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("Disney"))
                {
                    WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    SeriesDTOs.Add(SeriesDto);
                }
                else if (responseString.Contains("IMDB"))
                {
                    //WatchableDTO deserializedWatchable = JsonConvert.DeserializeObject<WatchableDTO>(responseString);
                    //SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(deserializedWatchable);
                    //SeriesDTOs.Add(SeriesDto);
                    Debug.WriteLine("APARECEU");
                }
            }
            return SeriesDTOs;
        }

        [HttpGet("series")]
        public async Task<List<SeriesDTO>> GetSeriesByRating(double min_rating)
        {
            List<SeriesDTO> SeriesDtos = new();
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
                    SeriesDTO SeriesDto = SeriesEntityMapper.MapToDTO(item);
                    SeriesDtos.Add(SeriesDto);
                }
            }

            var results = SeriesDtos.GroupBy(m => m.Platform).SelectMany(series => series).ToList();

            return results;
        }
    }
}
