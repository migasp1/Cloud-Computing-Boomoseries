namespace boomoseries_Books_api.Helpers
{
    public class URLHelper
    {
        private static readonly string GoodReadsBaseUrl = "https://localhost:5013/api/v1/GoodReads/books";

        public static string[] GetMicroservicesBaseURL()
        {
            var urls = new string[]
            {
                GoodReadsBaseUrl
            };

            return urls;
        }
    }
}
