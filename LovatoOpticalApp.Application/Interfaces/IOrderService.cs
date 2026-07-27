using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.DTOs.Order;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrder(CreateOrderRequestDto request);
        Task<OrderResponseDto?> GetOrderById(Guid orderId);
        Task<IEnumerable<OrderResponseDto>> GetOrders();
    }
}
