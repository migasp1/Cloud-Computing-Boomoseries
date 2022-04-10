using Microsoft.AspNetCore.Mvc;

namespace boomoseries_Search_api.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
