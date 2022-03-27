using boomoseries_Movies_api.Helpers;
using System.Collections.Generic;

namespace boomoseries_Movies_api.Services.REST_Communication
{
    public class RESTCommuniationService
    {
        private readonly IEnumerable<string> microservicesBaseURL = URLHelper.GetMicroservicesBaseURL();
    }
}
