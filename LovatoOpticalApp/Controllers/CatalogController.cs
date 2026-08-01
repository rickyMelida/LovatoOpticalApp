using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;

using LovatoOpticalApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CatalogController : AccessoryController
    {	
        public CatalogController(IFrameService frameService, IProductService productService, IAccessoryService accessoryService) : 
            base(frameService, productService, accessoryService)
        {
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

        [HttpGet]
        public async Task<IActionResult> SearchCatalog(string query)
        {
            var parameters = new PaginationParams { PageNumber = 1, PageSize = 10 };

            var products = String.IsNullOrEmpty(query)
                    ? await _productService.GetProducts(parameters)
                    : await _productService.SearchCatalog(query, parameters);

            ViewData["Products"] = products;

            return PartialView("Grid/_CatalogGrid");
        }
    }
}
