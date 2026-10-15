using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
	public class CrystalController: Controller
	{
		private readonly ICrystalPriceService _crystalPriceService;
		public CrystalController(ICrystalPriceService crystalPriceService)
		{
			_crystalPriceService = crystalPriceService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetCrystalPricesByType(string crystalType)
		{
			if (!Enum.TryParse(crystalType, out CrystalTypeEnum crystalTypeEnum))
			{
				return BadRequest("Invalid crystal type.");
			}

			var crystalPrices = await _crystalPriceService.GetCrystalPriceByTypeAsync(crystalTypeEnum);
			return Ok(crystalPrices);
		}
	}
}