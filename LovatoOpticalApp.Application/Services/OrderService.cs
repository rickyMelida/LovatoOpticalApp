using LovatoOpticalApp.Application.DTOs.Order;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence;
using LovatoOpticalApp.Persistence.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository<Frame> _frameRepository;
        private readonly ICrystalRepository _crystalRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository<Frame> frameRepository,
            ICrystalRepository crystalRepository)
        {
            _orderRepository      = orderRepository;
            _customerRepository   = customerRepository;
            _frameRepository      = frameRepository;
            _crystalRepository    = crystalRepository;
        }

        public async Task<OrderResponseDto> CreateOrder(CreateOrderRequestDto request)
        {
            // 1. Recuperar entidades requeridas
            var customer = await _customerRepository.GetCustomerDetails(request.CustomerId)
                ?? throw new KeyNotFoundException($"Cliente {request.CustomerId} no encontrado.");

            var frame = await _frameRepository.GetByIdAsync(request.FrameId)
                ?? throw new KeyNotFoundException($"Armazón {request.FrameId} no encontrado.");

            // 2. Recuperar cristales (opcionales por ojo)
            Crystal? crystalRight = null;
            Crystal? crystalLeft  = null;

            // 3. Construir la Order con las reglas del dominio
            var order = new OrderBuilder()
                .ForCustomer(customer)
                .WithFrame(frame)
                .WithRightCrystal(crystalRight!)
                .WithLeftCrystal(crystalLeft!)
                .WithObservations(request.Observations ?? string.Empty)
                .Build();

            // 4. Confirmar la orden
            order.Confirm();

            // 5. Generar y completar la orden de trabajo para el laboratorio
            var workDto = request.CrystalOrderWork;

           

            // 6. Persistir
            await _orderRepository.AddAsync(order);

            return MapToResponse(order);
        }

        public async Task<OrderResponseDto?> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            return order is null ? null : MapToResponse(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(MapToResponse);
        }

        private static OrderResponseDto MapToResponse(Order order) => new()
        {
            Id                 = order.Id,
            CreatedAt          = order.CreateAt,
            State              = order.State.ToString(),
            CustomerId         = order.Customer?.Id ?? Guid.Empty,
            CustomerName       = order.Customer?.Name ?? string.Empty,
            FrameName          = order.Frame?.Name ?? string.Empty,
            TotalPrice         = order.TotalPrice,
            CrystalOrderWorkId = order.CrystalOrderWork?.Id,
        };
    }
}
