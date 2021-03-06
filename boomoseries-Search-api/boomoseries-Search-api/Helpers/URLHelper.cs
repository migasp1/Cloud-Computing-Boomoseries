using System;
using System.Diagnostics;

namespace boomoseries_Search_api.Helpers
{
    public class URLHelper
    {

        public static string[] GetMicroservicesBaseURL()
        {
            return new string[]
                 {
                    Environment.GetEnvironmentVariable("MOVIES_HOST"),
                    Environment.GetEnvironmentVariable("SERIES_HOST"),
                    Environment.GetEnvironmentVariable("BOOKS_HOST"),
                 };
        }
    }
}
