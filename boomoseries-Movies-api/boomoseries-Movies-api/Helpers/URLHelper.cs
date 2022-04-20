namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        private static readonly string NetflixBaseUrl = "http://host.docker.internal:5000/api/v1/Netflix/movies";
        private static readonly string IMDBBaseUrl = "http://host.docker.internal:5002/api/v1/IMDB/movies";
        private static readonly string DisneyPlusBaseUrl = "http://host.docker.internal:5004/api/v1/Disney/movies";
        private static readonly string AmazonPrimeBaseUrl = "http://host.docker.internal:5006/api/v1/Amazon/movies";

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
