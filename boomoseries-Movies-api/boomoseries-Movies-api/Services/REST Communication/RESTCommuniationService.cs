using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<string> ObtainMovies()
        {
 
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

            foreach (var r in responses)
            {
                // Extract the message body
                r.EnsureSuccessStatusCode();
                string s = await r.Content.ReadAsStringAsync();
                if (s.Contains("Netflix"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no netflix");
                }
                if (s.Contains("Amazon"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Amazon Prime");
                }
                if (s.Contains("Disney"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Disney+");
                }
                if (s.Contains("IMDB"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no IMDB");
                }
            }

            return "Success!";
        }
        public async Task<string> ObtainSepcificMovie(string movieTitle)
        {

            //Makes the requests to different microservices
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requests = microservicesBaseURL.Select(url => httpClient.GetAsync(url + movieTitle)).ToList();

            //Wait for all the requests to finish
            await Task.WhenAll(requests);
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            //Get the responses
            var responses = requests.Select(task => task.Result);

            foreach (var r in responses)
            {
                // Extract the message body
                r.EnsureSuccessStatusCode();
                string s = await r.Content.ReadAsStringAsync();
                if (s.Contains("Netflix"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no netflix");
                }
                if (s.Contains("Amazon"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Amazon Prime");
                }
                if (s.Contains("Disney"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no Disney+");
                }
                if (s.Contains("IMDB"))
                {
                    System.Diagnostics.Debug.WriteLine("Encontramos um Filme no IMDB");
                }
            }

            return "Success!";
        }
    }
}
