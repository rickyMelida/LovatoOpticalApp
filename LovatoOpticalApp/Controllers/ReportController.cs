using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
