using Microsoft.AspNetCore.Mvc;
using System;

namespace bomoseries_Series_api.Helpers
{
    public class URLHelper
    {
        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
            {
                Environment.GetEnvironmentVariable("NETFLIX_HOST"),
                Environment.GetEnvironmentVariable("DISNEY_HOST"),
                Environment.GetEnvironmentVariable("AMAZON_HOST")
            };

            return urls;
        }
    }
}
