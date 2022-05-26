using System;

namespace boomoseries_Books_api.Helpers
{
    public class URLHelper
    {
        public static string GetMicroservicesBaseURL()
        {
            //Environment.GetEnvironmentVariable("GOODREADS_HOST");
            return "http://host.docker.internal:5012/api/v1/GoodReads";
        }
    }
}
