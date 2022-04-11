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
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly HttpClient httpClient = new();

        public RESTCommunicationService()
        {
        }

        public async Task<List<BookDTO>> ObtainBooks()
        {
            List<BookDTO> booksDtos = new();
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
            foreach (var response in responses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
                foreach (var item in deserializedBook)
                {
                    booksDtos.Add(item);
                }

            }

            return booksDtos;
        }

        public async Task<List<BookDTO>> ObtainSpecificBook(string bookTitle)
        {
            List<BookDTO> booksDtos = new();
            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + "/" + bookTitle)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);

            foreach (var response in responses)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                BookDTO deserializedBook = JsonConvert.DeserializeObject<BookDTO>(responseString);
                booksDtos.Add(deserializedBook);
            }

            return booksDtos;
        }

        public async Task<List<BookDTO>> ObtainBooksByRating(double min_rating)
        {
            List<BookDTO> booksDtos = new();
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
                List<BookDTO> deserializedBook = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
                foreach (var item in deserializedBook)
                {
                    booksDtos.Add(item);
                }
                
            }

            return booksDtos;
        }
    }
}
