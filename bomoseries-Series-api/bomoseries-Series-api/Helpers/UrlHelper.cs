using Microsoft.AspNetCore.Mvc;

namespace bomoseries_Series_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "http://host.docker.internal:5000/api/v1/Netflix/series";
        private static readonly string DisneyPlusBaseUrl = "http://host.docker.internal:5004/api/v1/Disney/series";
        private static readonly string AmazonPrimeBaseUrl = "http://host.docker.internal:5006/api/v1/Amazon/series";

        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
            {
                NetflixBaseUrl,
                DisneyPlusBaseUrl,
                AmazonPrimeBaseUrl
            };

            return urls;
        }
    }
}
