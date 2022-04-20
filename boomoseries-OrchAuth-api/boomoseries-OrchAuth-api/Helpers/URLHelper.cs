namespace boomoseries_OrchAuth_api.Helpers
{
    public class URLHelper
    {
        public static readonly string _baseUsersMicroserviceURL = "http://host.docker.internal:5020/Users";
        public static readonly string _baseUserPreferencesMicroserviceURL = "http://host.docker.internal:5024/UserPreferences/Favorites";
        public static readonly string _baseSearchMicroserviceURL = "http://host.docker.internal:5018/api/v1/Search";

    }
}
