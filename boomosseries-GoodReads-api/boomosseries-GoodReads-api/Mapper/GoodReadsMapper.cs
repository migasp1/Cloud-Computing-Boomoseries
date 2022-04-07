using boomosseries_GoodReads_api.DTOs;

namespace boomosseries_GoodReads_api.Mapper
{
    public class GoodReadsMapper
    {
        public static BooksDTO MapToDTO(Books books)
        {
            return new BooksDTO()
            {
                Id = books.Id,
                Title = books.Title,
                Author = books.Author,
                Pages = books.Pages,
                Rating = books.Rating,
            };
        }
    }
}
