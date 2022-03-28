using System.Collections.Generic;

namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "https://localhost:5001/api/v1";
        private static readonly string IMDBBaseUrl = "https://localhost:5003/api/v1";
        private static readonly string DisneyPlusBaseUrl = "https://localhost:5004/api/v1";
        private static readonly string AmazonPrimeBaseUrl = "https://localhost:5006/api/v1";

        public static List<string> GetMicroservicesBaseURL()
        {
            var urls = new List<string>
            {
                NetflixBaseUrl,
                IMDBBaseUrl,
                DisneyPlusBaseUrl,
                AmazonPrimeBaseUrl
            };

            return urls;
        }
    }
}
