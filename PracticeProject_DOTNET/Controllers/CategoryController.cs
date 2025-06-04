using Microsoft.AspNetCore.Mvc;

namespace PracticeProject_DOTNET.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
