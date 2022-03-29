using boomoseries_IMDB_api.DTOs;

namespace boomoseries_IMDB_api.Mapper
{
    public class IMDBMapper
    {
        public static WatchableDTO MapToDTO(IMDBWatchable watchable)
        {
            return new WatchableDTO()
            {
                Id = watchable.Id,
                Title = watchable.Title,
                Rating = watchable.Rating,
                Director = watchable.Director,
                Cast = watchable.Cast,
                Type = watchable.Type,
                Platform = "IMDB"
            };
        }
    }
}
