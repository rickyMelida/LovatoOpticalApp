using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
