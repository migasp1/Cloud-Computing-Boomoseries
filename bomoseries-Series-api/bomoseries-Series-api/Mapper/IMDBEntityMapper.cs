using bomoseries_Series_api.DTOs;

namespace bomoseries_Series_api.Mapper
{
    public class IMDBEntityMapper
    {
        public static IMDBDTO MapToDTO(IMDBWatchableDTO watchable)
        {
            return new IMDBDTO()
            {
                Title = watchable.Title,    
                Director = watchable.Director,
                Cast = watchable.Cast
            };
        }
    }
}
