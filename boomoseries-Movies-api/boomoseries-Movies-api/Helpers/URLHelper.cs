namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "https://localhost:5001/api/v1/Netflix/movies";
        private static readonly string DisneyPlusBaseUrl = "https://localhost:5005/api/v1/Disney/movies";
        private static readonly string AmazonPrimeBaseUrl = "https://localhost:5007/api/v1/Amazon/movies";

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
