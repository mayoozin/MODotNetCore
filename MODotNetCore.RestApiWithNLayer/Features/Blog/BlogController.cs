using Microsoft.AspNetCore.Mvc;

namespace MODotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
