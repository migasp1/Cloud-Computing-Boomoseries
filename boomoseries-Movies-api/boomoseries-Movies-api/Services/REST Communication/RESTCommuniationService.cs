using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Helpers;
using System;
using System.Collections.Generic;
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

        public async Task<string> ObtainSepcificMovie(string movieTitle)
        {
            HttpResponseMessage netflixResponse = await httpClient.GetAsync(microservicesBaseURL[0] + movieTitle);
            //HttpResponseMessage disneyResponse = await httpClient.GetAsync(microservicesBaseURL[2] + movieTitle);
            //HttpResponseMessage amazonResponse = await httpClient.GetAsync(microservicesBaseURL[3] + movieTitle);

            //Makes the requests to different microservices
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);

            //Get the responses
            var responses = requests.Select(task => task.Result);

            foreach (var r in responses)
            {
                // Extract the message body
                r.EnsureSuccessStatusCode();
                string s = await r.Content.ReadAsStringAsync();
                if (s.Contains("Netflix"))
                {
                    Console.WriteLine("Encontramos um Filme no netflix");
                }
                else if(s.Contains("Amazon"))
                {
                    Console.WriteLine("Encontramos um Filme no Amazon Prime");
                }
                else if (s.Contains("Disney"))
                {
                    Console.WriteLine("Encontramos um Filme no Disney+");
                }
                else if (s.Contains("IMDB"))
                {
                    Console.WriteLine("Encontramos um Filme no IMDB");
                }
            }

            netflixResponse.EnsureSuccessStatusCode();
            string responseBody = await netflixResponse.Content.ReadAsStringAsync();
            System.Console.WriteLine(responseBody);
            return responseBody;
        }
    }
}
