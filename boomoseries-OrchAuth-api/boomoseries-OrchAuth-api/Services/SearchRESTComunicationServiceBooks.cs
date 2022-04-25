using boomoseries_OrchAuth_api.DTOs;
using boomoseries_OrchAuth_api.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Services
{
    public class SearchRESTComunicationServiceBooks : ISearchCommunicationServiceBooks
    {
        private static readonly string microservicesBaseURL = Environment.GetEnvironmentVariable("SEARCH_HOST");
        private static readonly HttpClient httpClient = new();

        public async Task<List<BookDTO>> ObtainRandomBooks(string type)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL + "/random" + "?type=" + type);

            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;

            var responseString = await response.Content.ReadAsStringAsync();
            List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            foreach (var item in deserializedBook)
            {
                booksDtos.Add(item);
            }
            return booksDtos;
        }

        public async Task<List<BookDTO>> ObtainSpecificBook(string type, string book_title)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL + "/" + book_title + "?type=" + type);

            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;

            var responseString = await response.Content.ReadAsStringAsync();
            List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            foreach (var item in deserializedBook)
            {
                booksDtos.Add(item);
            }
            return booksDtos;
        }

        public async Task<List<BookDTO>> ObtainBooksByRating(string type, double minRating)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL + "?type=" + type + "&minRating=" + minRating);

            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var response = request.Result;

            var responseString = await response.Content.ReadAsStringAsync();
            List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            foreach (var item in deserializedBook)
            {
                booksDtos.Add(item);
            }
            return booksDtos;
        }
    }
}
