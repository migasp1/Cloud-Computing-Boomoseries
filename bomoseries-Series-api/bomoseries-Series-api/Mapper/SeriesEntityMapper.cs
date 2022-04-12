using Microsoft.AspNetCore.Mvc;
using bomoseries_Series_api.DTOs;

namespace bomoseries_Series_api.Mapper
{
    public class SeriesEntityMapper
    {
        public static SeriesDTO MapToDTO(WatchableDTO watchable)
        {
            return new SeriesDTO()
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
