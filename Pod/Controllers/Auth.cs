using Microsoft.AspNetCore.Mvc;

namespace Pod.Controllers
{
    public class Auth : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
    