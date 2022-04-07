using boomoseries_Movies_api.DTOs;

namespace boomoseries_Movies_api.Mapper
{
    public class MovieEntityMapper
    {
        public static MovieDTO MapToDTO(WatchableDTO watchable)
        {
            return new MovieDTO()
            {
                Title = watchable.Title,
                Date = watchable.Date,
                Rating = watchable.Rating,
                Type = watchable.Type,  
                Platform = watchable.Platform
            };
        }
    }
}
