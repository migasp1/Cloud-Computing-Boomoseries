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

    public class RESTBooksCommunicationService : ICommunicationServiceBooks
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private readonly HttpClient httpClient;

        public RESTBooksCommunicationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<BookDTO>> ObtainRandomBooks()
        {
            List<BookDTO> booksDtos = new();

            var request = httpClient.GetAsync(microservicesBaseURL[2] + "/random");

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

        public async Task<List<BookDTO>> ObtainSpecificBook(string book_title)
        {
            List<BookDTO> booksDtos = new();

            var request = httpClient.GetAsync(microservicesBaseURL[2] + "/" + book_title);

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

        public async Task<List<BookDTO>> ObtainBooksByRating(double min_rating)
        {
            List<BookDTO> booksDtos = new();

            var request = httpClient.GetAsync(microservicesBaseURL[2] + "?min_rating=" + min_rating);

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