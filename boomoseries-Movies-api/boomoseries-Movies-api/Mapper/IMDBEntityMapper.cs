using boomoseries_Movies_api.DTOs;

namespace boomoseries_Movies_api.Mapper
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
