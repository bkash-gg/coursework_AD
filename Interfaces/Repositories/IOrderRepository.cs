using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<OrderItem?> GetOrderItemAsync(Guid orderId, Guid bookId);
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
        Task<bool> RemoveOrderItemAsync(Guid orderId, Guid bookId);
        Task<int> GetUserOrderCountAsync(Guid userId);
    }
}