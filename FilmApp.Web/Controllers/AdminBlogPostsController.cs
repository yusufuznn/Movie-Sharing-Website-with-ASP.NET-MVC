using Microsoft.AspNetCore.Mvc;

namespace FilmApp.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

    }
}
