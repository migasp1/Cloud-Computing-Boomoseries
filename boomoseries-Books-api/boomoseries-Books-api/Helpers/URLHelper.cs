using System;

namespace boomoseries_Books_api.Helpers
{
    public class URLHelper
    {
        public static string GetMicroservicesBaseURL()
        {
            return Environment.GetEnvironmentVariable("GOODREADS_HOST");
        }
    }
}
