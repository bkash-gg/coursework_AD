using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using AD_Coursework.DTOs.Checkout;

namespace AD_Coursework.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCompletedOrderCountAsync()
        {
            return await _context.Orders
                .Where(o => o.Status == OrderStatus.Completed)
                .CountAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByClaimCodeAsync(Guid userId, string claimCode)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.ClaimCode == claimCode);
        }

        public async Task<int> GetSuccessfulOrderCountAsync(Guid userId)
        {
            return await _context.Orders
                .CountAsync(o => o.UserId == userId && o.Status == OrderStatus.Completed);
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Guid orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Book)
                .ToListAsync();
        }

        public async Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(ci => ci.Book)
                .Include(c => c.User) 
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty");
            }

            var summary = new CheckoutSummaryDto
            {
                Items = cart.Items.Select(ci => new CheckoutItemDto
                {
                    BookId = ci.BookId,
                    BookTitle = ci.Book?.Title ?? string.Empty,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    LineTotal = ci.Quantity * ci.UnitPrice,
                    ImageUrl = ci.Book?.CoverImageUrl ?? string.Empty
                }).ToList(),
                Subtotal = cart.Items.Sum(ci => ci.Quantity * ci.UnitPrice),
                FullName = cart.User.FullName,
                Email = cart.User.Email,
                PhoneNumber = cart.User.PhoneNumber,
                Address = cart.User.Address
            };

            summary.EligibleForBulkDiscount = cart.Items.Sum(i => i.Quantity) >= 5;

            summary.LoyaltyOrderCount = await _context.Orders
                .CountAsync(o => o.UserId == userId && o.Status == OrderStatus.Completed);
            summary.EligibleForLoyaltyDiscount = summary.LoyaltyOrderCount >= 10;

            summary.DiscountPercentage = 0;
            if (summary.EligibleForBulkDiscount)
            {
                summary.DiscountPercentage += 5;
            }
            if (summary.EligibleForLoyaltyDiscount)
            {
                summary.DiscountPercentage += 10;
            }

            summary.DiscountAmount = summary.Subtotal * (summary.DiscountPercentage / 100);
            summary.TotalAmount = summary.Subtotal - summary.DiscountAmount;

            return summary;
        }
    }
}