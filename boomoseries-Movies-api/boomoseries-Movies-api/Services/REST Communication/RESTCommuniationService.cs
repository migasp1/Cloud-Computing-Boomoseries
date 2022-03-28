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
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5001/api/v1/Netflix/movies/" + movieTitle);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(responseBody);
            return responseBody;
        } 
    }
}
