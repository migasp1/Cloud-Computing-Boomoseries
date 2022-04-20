﻿namespace boomoseries_Search_api.Helpers
{
    public class URLHelper
    {
            private static readonly string MoviesBaseUrl = "http://host.docker.internal:5008/api/v1/Movies";
            private static readonly string SeriesBaseUrl = "http://host.docker.internal:5016/api/v1/Series";
            private static readonly string BooksBaseUrl = "http://host.docker.internal:5014/api/v1/Books";

        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
            {
                MoviesBaseUrl,
                SeriesBaseUrl,
                BooksBaseUrl,
            };

            return urls;
        }
    }
}
