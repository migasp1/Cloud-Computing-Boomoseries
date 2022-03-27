using System.Collections.Generic;

namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "https://localhost:5003/";
        private static readonly string IMDBBaseUrl = "https://localhost:5001/";
        private static readonly string DisneyPlusBaseUrl = "https://localhost:5004/";
        private static readonly string AmazonPrimeBaseUrl = "https://localhost:5006/";

        public static IEnumerable<string> GetMicroservicesBaseURL()
        {
            var urls = new List<string>();

            urls.Add(NetflixBaseUrl);
            urls.Add(IMDBBaseUrl);
            urls.Add(DisneyPlusBaseUrl);
            urls.Add(AmazonPrimeBaseUrl);

            return urls;
        }
    }
}
