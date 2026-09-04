using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Order;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class OrderWorkController : Controller
    {
        private readonly ICrystalOrderWorkService _crystalOrderWorkService;

        public OrderWorkController(ICrystalOrderWorkService crystalOrderWorkService)
        {
            _crystalOrderWorkService = crystalOrderWorkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Crea y persiste una nueva orden de trabajo para el laboratorio.
        /// POST /OrderWork/Create
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrystalOrderWorkRequestDto request)
        {
            if (request is null)
                return BadRequest(new { message = "La solicitud no puede estar vacía." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _crystalOrderWorkService.CreateCrystalOrderWork(request);
                return Ok(new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { message = "Error al guardar la orden de trabajo.", detail });
            }
        }

        /// <summary>
        /// Obtiene las órdenes de trabajo paginadas para la grilla.
        /// GET /OrderWork/GetAll?pageNumber=1&amp;pageSize=10
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var parameters = new PaginationParams
            {
                PageNumber = pageNumber > 0 ? pageNumber : 1,
                PageSize = pageSize > 0 ? pageSize : 10
            };

            var result = await _crystalOrderWorkService.GetCrystalOrderWorks(parameters);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle de una orden de trabajo.
        /// GET /OrderWork/GetById?id={guid}
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "El ID no es válido." });

            var result = await _crystalOrderWorkService.GetById(id);
            if (result is null)
                return NotFound(new { message = $"Orden de trabajo {id} no encontrada." });

            return Ok(result);
        }

        /// <summary>
        /// Obtiene el próximo índice autoincremental para la nueva orden de trabajo.
        /// GET /OrderWork/GetNextIndex
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetNextIndex()
        {
            var nextIndex = await _crystalOrderWorkService.GetNextIndex();
            return Ok(new { nextIndex });
        }
    }
}
