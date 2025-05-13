using AD_Coursework.DTOs.Order;

namespace AD_Coursework.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<OrderDto> UpdateOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto);
        Task<bool> CancelOrderAsync(Guid orderId, string cancellationReason);
    }
}