namespace boomoseries_Books_api.Helpers
{
    public class URLHelper
    {
        private static readonly string GoodReadsBaseUrl = "http://host.docker.internal:5012/api/v1/GoodReads/books";

        public static string GetMicroservicesBaseURL()
        {
            return GoodReadsBaseUrl;
        }
    }
}
