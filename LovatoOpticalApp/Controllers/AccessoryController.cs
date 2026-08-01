using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
	public class AccessoryController : FrameController
	{
		protected readonly IAccessoryService _accessoryService;
       

		public AccessoryController(IFrameService frameService, IProductService productService, IAccessoryService accessoryService) : 
		base(frameService, productService)
		{
			_accessoryService = accessoryService;
		}

		public AccessoryController(IFrameService frameService, IProductService productService) : base(frameService, productService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetAccessories()
		{
			var accessories = await _accessoryService.GetAllAccessories();

			return Ok(accessories);
		}

		[HttpGet]
		public async Task<IActionResult> GetAccessoryById(string id)
		{
			if (!Guid.TryParse(id, out Guid guidId))
				return BadRequest("Id no valido");

			var accessory = await _accessoryService.GetAccessoryById(guidId);

			return Ok(accessory);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAccessory([FromBody] AccessoryRequestDto? accessory)
		{
			if (accessory is null)
				return BadRequest("El cuerpo de la solicitud no es válido.");

			var result = await _accessoryService.CreateAccessory(accessory);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAccessory([FromBody] AccessoryRequestDto? accessory)
		{
			if (accessory is null)
				return BadRequest("El cuerpo de la solicitud no es válido.");

			var result = await _accessoryService.UpdateAccessory(accessory);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAccessory(string id)
		{
			if (!Guid.TryParse(id, out Guid guidId))
				return BadRequest("Id no valido");

			var result = await _accessoryService.DeleteAccessory(guidId);

			return Ok(result);
		}
	}
}