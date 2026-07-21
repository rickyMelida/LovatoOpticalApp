using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductService _productService;
        public InventoryController(IProductService productService)
        {
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
        public async Task<IActionResult> AddStock(Guid productId, ProductTypeEnum productType, int quantityToAdd)
        {
            if (quantityToAdd <= 0)
            {
                TempData["Error"] = "La cantidad debe ser mayor a cero.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _productService.AddStock(productId, productType, quantityToAdd);

            if (result.Status == 200)
                TempData["Success"] = result.Message;
            else
                TempData["Error"] = result.Message;

            return RedirectToAction(nameof(Index));
        }
    }
}
