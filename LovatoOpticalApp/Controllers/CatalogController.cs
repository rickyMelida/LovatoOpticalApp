using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IFrameService _frameService;
		private readonly IProductService _productService;
		

        public CatalogController(IFrameService frameService, IProductService productService)
        {
            _frameService = frameService;
			_productService = productService;
        }

        public async Task<IActionResult> Index()
        {
			var paginationParams = new PaginationParams
			{
				PageNumber = 1,
				PageSize = 10
			};

			var result = await _productService.GetProducts(paginationParams);
			ViewData["Products"] = result;
			
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFrame([FromBody] FrameRequestDto? frame)
        {
            if (frame is null)
            	return BadRequest("El cuerpo de la solicitud no es válido.");
            

            var result = await _frameService.CreateFrame(frame);
            return Ok(result);
        }

		[HttpGet]
		public async Task<ActionResult<FrameResponseDto>> GetProductDetails(string productId, ProductTypeEnum productType)
		{
			if(String.IsNullOrEmpty(productId) || !Guid.TryParse(productId, out Guid parsedProductId))
				return BadRequest("El ID del producto no es válido.");

			if(!Enum.IsDefined(typeof(ProductTypeEnum), productType))
				return BadRequest("El tipo de producto no es válido.");


			var frameDetails = await _productService.GetProductDetails(parsedProductId, productType);

			return Ok(frameDetails);
		}
    }
}
