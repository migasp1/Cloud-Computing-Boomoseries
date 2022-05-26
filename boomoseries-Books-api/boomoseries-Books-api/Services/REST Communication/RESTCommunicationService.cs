using boomoseries_Books_api.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using boomoseries_Books_api.DTOs;

namespace boomoseries_Books_api.Services.REST_Communication
{
    public class RESTCommunicationService : ICommunicationService
    {
        private static readonly string microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private readonly HttpClient httpClient;

        public RESTCommunicationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;   
        }

        public async Task<List<BookDTO>> ObtainRandomBooks()
        {
            List<BookDTO> booksDtos = new();
           
            var request = httpClient.GetAsync(microservicesBaseURL + "/random");

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

        public async Task<List<BookDTO>> ObtainSpecificBook(string bookTitle)
        {
            List<BookDTO> booksDtos = new();
           
            var request = httpClient.GetAsync(microservicesBaseURL + "/" + bookTitle);

            //Get the responses
            var response = request.Result;

            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return booksDtos;
            }
            BookDTO deserializedBook = JsonConvert.DeserializeObject<BookDTO>(responseString);
            booksDtos.Add(deserializedBook);

            return booksDtos;
        }

        public async Task<List<BookDTO>> ObtainBooksByRating(double min_rating)
        {
            List<BookDTO> booksDtos = new();
           
            var request = httpClient.GetAsync(microservicesBaseURL + "?min_rating=" + min_rating);

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
