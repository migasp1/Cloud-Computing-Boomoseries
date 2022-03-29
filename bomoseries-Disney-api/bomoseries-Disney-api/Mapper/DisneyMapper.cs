using bomoseries_Disney_api.DTOs;
using boomoseries_Disney_api;

namespace bomoseries_Disney_api.Mapper
{
    public class DisneyMapper
    {
        public static WatchableDTO MapToDTO(Watchable watchable)
        {
            return new WatchableDTO()
            {
                Id = watchable.Id,
                Title = watchable.Title,
                Rating = watchable.Rating,
                Date = watchable.Date,
                Type = watchable.Type,
                Platform = "Disney"
            };
        }
    }
}
