using LovatoOpticalApp.Application.DTOs.Order;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        /// <summary>
        /// Crea una nueva orden con su orden de trabajo para el laboratorio.
        /// POST /Order/Create
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _orderService.CreateOrder(request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el detalle de una orden por su ID.
        /// GET /Order/GetById?orderId={id}
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest("El ID de la orden no es válido.");

            var result = await _orderService.GetOrderById(orderId);

            if (result is null)
                return NotFound(new { message = $"Orden {orderId} no encontrada." });

            return Ok(result);
        }

        /// <summary>
        /// Lista todas las órdenes.
        /// GET /Order/GetAll
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }
    }
}
