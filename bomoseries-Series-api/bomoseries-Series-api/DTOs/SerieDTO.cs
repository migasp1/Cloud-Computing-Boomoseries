using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bomoseries_Series_api.DTOs
{
    public class SerieDTO
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public string Platform { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
    }
}
