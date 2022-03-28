using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Services.REST_Communication
{
    public class RESTCommuniationService : ICommunicationService
    {
        private static readonly List<string> microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly HttpClient httpClient = new();

        public RESTCommuniationService()
        {
        }

        public async Task<string> ObtainSepcificMovie(string movieTitle)
        {
            HttpResponseMessage netflixResponse = await httpClient.GetAsync(microservicesBaseURL[0] + movieTitle);
            //HttpResponseMessage disneyResponse = await httpClient.GetAsync(microservicesBaseURL[2] + movieTitle);
            //HttpResponseMessage amazonResponse = await httpClient.GetAsync(microservicesBaseURL[3] + movieTitle);
            netflixResponse.EnsureSuccessStatusCode();
            string responseBody = await netflixResponse.Content.ReadAsStringAsync();
            System.Console.WriteLine(responseBody);
            return responseBody;
        } 
    }
}
