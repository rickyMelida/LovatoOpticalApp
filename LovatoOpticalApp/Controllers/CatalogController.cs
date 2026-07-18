using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IFrameService _frameService;
		private readonly IProductService _productService;
		public PagedResult<ProductResponse> Products { get; set; } = new PagedResult<ProductResponse>();

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
			
            return View(Products);
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
		public async Task<ActionResult<PagedResult<FrameResponseDto>>> GetProductCatalog(
			[FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var paginationParams = new PaginationParams
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			};

			var result = await _productService.GetProducts(paginationParams);
			Products = result;
			return Ok(result);
		}
    }
}
