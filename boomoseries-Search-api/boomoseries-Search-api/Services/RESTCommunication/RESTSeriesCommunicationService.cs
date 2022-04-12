using boomoseries_Search_api.Helpers;
using System.Net.Http;

namespace boomoseries_Search_api.Services.RESTCommunication
{
    public class RESTSeriesCommunicationService
    {
        private static readonly string[] microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
        private static readonly HttpClient httpClient = new();
    }
}
