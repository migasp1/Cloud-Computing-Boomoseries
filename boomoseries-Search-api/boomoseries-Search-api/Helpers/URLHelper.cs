using System;
using System.Diagnostics;

namespace boomoseries_Search_api.Helpers
{
    public class URLHelper
    {
        //var urls = new string[]
        //    {
        //        Environment.GetEnvironmentVariable("MOVIES_HOST"),
        //        Environment.GetEnvironmentVariable("SERIES_HOST"),
        //        Environment.GetEnvironmentVariable("BOOKS_HOST"),
        //    };
        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
            {
                //"http://host.docker.internal:5008/api/v1/Movies",
                //"http://host.docker.internal:5016/api/v1/Series",
                //"http://host.docker.internal:5014/api/v1/Books",
                "https://localhost:5009/api/v1/Movies",
                "https://localhost:5017/api/v1/Series",
                "https://localhost:5015/api/v1/Books"
            };
            return urls;
        }
    }
}
