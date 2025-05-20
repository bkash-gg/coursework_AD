using AD_Coursework.DTOs.Checkout;
using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<Order?> GetOrderByClaimCodeAsync(Guid userId, string claimCode);
        Task<int> GetSuccessfulOrderCountAsync(Guid userId);
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Guid orderId);
        Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(Guid userId);
        Task<int> GetCompletedOrderCountAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}