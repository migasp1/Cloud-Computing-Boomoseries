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
        private static readonly string microservicesBaseURL = "http://host.docker.internal:5018/api/v1/Search";
        //private static readonly string microservicesBaseURL = "https://localhost:5019/api/v1/Search";
        //private static readonly string microservicesBaseURL = Environment.GetEnvironmentVariable("SEARCH_HOST");
        private readonly HttpClient httpClient;

        public SearchRESTComunicationServiceBooks(
            HttpClient httpClient
            )
        {
            this.httpClient = httpClient;
        }

        public async Task<object> ObtainRandomBooks(string type)
        {
            List<BookDTO> booksDtos = new();
            var request = httpClient.GetAsync(microservicesBaseURL + "/random" + "?type=" + type);
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            var responseString = await response.Content.ReadAsStringAsync();
            List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            foreach (var item in deserializedBook)
            {
                booksDtos.Add(item);
            }
            return booksDtos;
        }

        public async Task<object> ObtainSpecificBook(string type, string book_title)
        {
            List<BookDTO> booksDtos = new();
            var request = httpClient.GetAsync(microservicesBaseURL + "/" + book_title + "?type=" + type);
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            var responseString = await response.Content.ReadAsStringAsync();
            List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            foreach (var item in deserializedBook)
            {
                booksDtos.Add(item);
            }
            return booksDtos;
        }

        public async Task<object> ObtainBooksByRating(string type, double minRating)
        {
            List<BookDTO> booksDtos = new();
            var request = httpClient.GetAsync(microservicesBaseURL + "?type=" + type + "&minRating=" + minRating);
            var response = request.Result;
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
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
