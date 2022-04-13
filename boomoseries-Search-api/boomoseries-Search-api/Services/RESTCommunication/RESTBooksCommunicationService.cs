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
        private static readonly HttpClient httpClient = new();

        public RESTBooksCommunicationService()
        {
        }

        public async Task<List<BookDTO>> ObtainRandomBooks()
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[2] + "/random");

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

        public async Task<List<BookDTO>> ObtainSpecificBook(string book_title)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[2] + "/" + book_title);

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

        public async Task<List<BookDTO>> ObtainBooksByRating(double min_rating)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = httpClient.GetAsync(microservicesBaseURL[2] + "?min_rating=" + min_rating);

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