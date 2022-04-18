namespace boomoseries_Search_api.Helpers
{
    public class URLHelper
    {
        private static readonly string MoviesBaseUrl = "https://localhost:5009/api/v1/Movies";
        private static readonly string SeriesBaseUrl = "https://localhost:5017/api/v1/Series";
        private static readonly string BooksBaseUrl = "https://localhost:5015/api/v1/Books";

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
