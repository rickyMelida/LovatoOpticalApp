using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class FrameController : Controller
    {
        protected readonly IFrameService _frameService;
        protected readonly IProductService _productService;

        public FrameController(IFrameService frameService, IProductService productService)
        {
            _frameService = frameService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FrameResponseDto>>> GetFrames()
        {
            var result = await _frameService.GetFrames();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<FrameResponseDto>> GetFrameById(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
                return BadRequest("Id no valido");

            var result = await _frameService.GetFrameById(guidId);

            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> CreateFrame([FromBody] FrameRequestDto? frame)
        {
            if (frame is null)
                return BadRequest("El cuerpo de la solicitud no es válido.");


            var result = await _frameService.CreateFrame(frame);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFrame([FromBody] FrameRequestDto? frame)
        {
            if (frame is null)
                return BadRequest("El cuerpo de la solicitud no es válido.");


            var result = await _frameService.UpdateFrame(frame);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string productId, ProductTypeEnum productType)
        {
            if (String.IsNullOrEmpty(productId) || !Guid.TryParse(productId, out Guid parsedProductId))
                return BadRequest("El ID del producto no es válido.");

            if (!Enum.IsDefined(typeof(ProductTypeEnum), productType))
                return BadRequest("El tipo de producto no es válido.");

            var result = await _productService.DeleteProduct(parsedProductId, productType);

            return Ok(result);

        }
    }
}
