namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "http://localhost:5000/api/v1/Netflix/movies";
        private static readonly string DisneyPlusBaseUrl = "http://localhost:5004/api/v1/Disney/movies";
        private static readonly string AmazonPrimeBaseUrl = "http://localhost:5006/api/v1/Amazon/movies";

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
