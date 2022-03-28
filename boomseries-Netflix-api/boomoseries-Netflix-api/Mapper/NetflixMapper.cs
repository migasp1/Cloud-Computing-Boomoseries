using boomoseries_Netflix_api.DTOs;

namespace boomoseries_Netflix_api.Mapper
{
    public class NetflixMapper
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
