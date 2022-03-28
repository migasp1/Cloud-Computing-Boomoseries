using boomoseries_Amazon_api.DTOs;

namespace boomoseries_Amazon_api.Mapper
{
    public class AmazonMapper
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
                Platform = "Netflix"
            };
        }
    }
}
