using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
	public class CrystalController: Controller
	{
		public CrystalController()
		{
			
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}