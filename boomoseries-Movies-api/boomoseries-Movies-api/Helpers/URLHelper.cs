using System;

namespace boomoseries_Movies_api.Helpers
{
    public class URLHelper
    {
        public static string[] GetMicroservicesBaseURL()
        {
            /*
            var urls = new string[]
            {
                Environment.GetEnvironmentVariable("NETFLIX_HOST"),
                Environment.GetEnvironmentVariable("DISNEY_HOST"),
                Environment.GetEnvironmentVariable("AMAZON_HOST")
            };
            */
            var urls = new string[]
            {
                "http://host.docker.internal:5000/api/v1/Netflix/movies",
                "http://host.docker.internal:5004/api/v1/Disney/movies",
                "http://host.docker.internal:5006/api/v1/Amazon/movies"
                //"https://localhost:5001/api/v1/Netflix/movies",
                //"https://localhost:5005/api/v1/Disney/movies",
                //"https://localhost:5007/api/v1/Amazon/movies"
            };

            return urls;
        }
    }
}
