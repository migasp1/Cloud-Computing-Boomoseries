using System;

namespace boomoseries_Books_api.Helpers
{
    public class URLHelper
    {
        public static string GetMicroservicesBaseURL()
        {
            //Environment.GetEnvironmentVariable("GOODREADS_HOST");
            //return "https://localhost:5013/api/v1/GoodReads/books";
            return Environment.GetEnvironmentVariable("GOODREADS_HOST");//"http://host.docker.internal:5012/api/v1/GoodReads/books";
        }
    }
}
