using Microsoft.AspNetCore.Mvc;

namespace bomoseries_Series_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "https://localhost:5001/api/v1/Netflix/series";
        private static readonly string IMDBBaseUrl = "https://localhost:5003/api/v1/IMDB/series";
        private static readonly string DisneyPlusBaseUrl = "https://localhost:5005/api/v1/Disney/series";
        private static readonly string AmazonPrimeBaseUrl = "https://localhost:5007/api/v1/Amazon/series";

        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
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
