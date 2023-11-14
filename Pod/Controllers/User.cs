using Microsoft.AspNetCore.Mvc;

namespace Pod.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
