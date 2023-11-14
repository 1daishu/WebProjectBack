using Microsoft.AspNetCore.Mvc;

namespace Pod.Controllers
{
    public class Cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
