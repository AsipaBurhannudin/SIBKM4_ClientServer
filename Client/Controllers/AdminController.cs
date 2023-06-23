using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }
    }
}
