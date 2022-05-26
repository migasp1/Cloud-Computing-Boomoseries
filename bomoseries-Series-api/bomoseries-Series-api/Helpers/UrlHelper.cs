using Microsoft.AspNetCore.Mvc;
using System;

namespace bomoseries_Series_api.Helpers
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
                "http://host.docker.internal:5000/api/v1/Netflix",
                "http://host.docker.internal:5004/api/v1/Disney",
                "http://host.docker.internal:5006/api/v1/Amazon"
            };

            return urls;
        }
    }
}
