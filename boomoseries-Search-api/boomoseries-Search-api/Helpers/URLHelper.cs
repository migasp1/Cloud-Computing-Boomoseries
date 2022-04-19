namespace boomoseries_Search_api.Helpers
{
    public class URLHelper
    {
            private static readonly string MoviesBaseUrl = "http://localhost:5008/api/v1/Movies";
            private static readonly string SeriesBaseUrl = "http://localhost:5016/api/v1/Series";
            private static readonly string BooksBaseUrl = "http://localhost:5014/api/v1/Books";

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
