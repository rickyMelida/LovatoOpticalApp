using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
