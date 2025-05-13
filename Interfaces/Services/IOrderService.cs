using AD_Coursework.DTOs.Order;
using System;
using System.Threading.Tasks;

namespace AD_Coursework.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(Guid userId, OrderCreateDto orderCreateDto);
        Task<OrderDto> GetOrderByIdAsync(Guid userId, Guid orderId);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> CancelOrderAsync(Guid userId, Guid orderId, string cancellationReason);
        Task<OrderDto> ProcessOrderAsync(Guid userId, string claimCode);
        Task<OrderDto> CompleteOrderAsync(Guid userId, string claimCode);
    }
}