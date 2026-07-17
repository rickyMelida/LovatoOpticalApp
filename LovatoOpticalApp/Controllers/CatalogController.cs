using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IFrameService _frameService;

        public CatalogController(IFrameService frameService)
        {
            _frameService = frameService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFrame([FromBody] FrameRequestDto frame)
        {
            var result = await _frameService.CreateFrame(frame);
            return Ok(result);
        }
    }
}
