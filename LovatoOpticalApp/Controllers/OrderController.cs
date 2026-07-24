using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

    }
}
