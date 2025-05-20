using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Order;
using AD_Coursework.DTOs.OrderItem;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(MapToOrderDto);
        }

        public async Task<OrderDto> PlaceOrderAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Your cart is empty. Please add items before placing an order.");
            }

            foreach (var cartItem in cart.Items)
            {
                var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId);
                if (book == null || !book.IsAvailable)
                {
                    throw new InvalidOperationException($"The book '{cartItem.Book?.Title}' is no longer available.");
                }
            }

            decimal discountPercentage = 0;
            bool usedBulkDiscount = false;
            bool usedLoyaltyDiscount = false;

            if (cart.Items.Sum(i => i.Quantity) >= 5)
            {
                discountPercentage += 5;
                usedBulkDiscount = true;
            }

            var successfulOrderCount = await _orderRepository.GetSuccessfulOrderCountAsync(userId);
            if (successfulOrderCount >= 10)
            {
                discountPercentage += 10;
                usedLoyaltyDiscount = true;
            }

            var subtotal = cart.Items.Sum(i => i.Quantity * i.UnitPrice);
            var discountAmount = subtotal * (discountPercentage / 100);
            var totalAmount = subtotal - discountAmount;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Subtotal = subtotal,
                DiscountAmount = discountAmount,
                DiscountPercentage = discountPercentage,
                TotalAmount = totalAmount,
                UsedBulkDiscount = usedBulkDiscount,
                UsedLoyaltyDiscount = usedLoyaltyDiscount,
                ClaimCode = GenerateClaimCode()
            };

            foreach (var cartItem in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice,
                    LineTotal = cartItem.Quantity * cartItem.UnitPrice
                });
            }

            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            await _cartRepository.ClearCartAsync(cart.Id);

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Send confirmation email
            await _emailService.SendOrderConfirmationEmail(user.Email, createdOrder);

            return MapToOrderDto(createdOrder);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid userId, Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null || order.UserId != userId)
            {
                throw new KeyNotFoundException("Order not found");
            }

            return MapToOrderDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapToOrderDto);
        }

        public async Task<OrderDto> CancelOrderAsync(Guid userId, Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null || order.UserId != userId)
            {
                throw new KeyNotFoundException("Order not found");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Only pending orders can be cancelled");
            }

            order.Status = OrderStatus.Cancelled;
            order.CancellationDate = DateTime.UtcNow;

            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return MapToOrderDto(updatedOrder);
        }

        public async Task<bool> PlaceOrderAgain(Guid userId, Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null || order.UserId != userId)
            {
                throw new KeyNotFoundException("Order not found");
            }

            if (order.Status != OrderStatus.Cancelled)
            {
                throw new InvalidOperationException("Only cancelled orders can be placed again");
            }

            order.Status = OrderStatus.Pending;
            order.CancellationDate = DateTime.UtcNow;

            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return true;
        }

        public async Task<OrderDto> ProcessOrderAsync(Guid userId, string claimCode)
        {
            var order = await _orderRepository.GetOrderByClaimCodeAsync(userId, claimCode);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found with the provided claim code");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Order has already been processed");
            }

            return MapToOrderDto(order);
        }

        public async Task<OrderDto> CompleteOrderAsync(Guid userId, string claimCode)
        {
            var order = await _orderRepository.GetOrderByClaimCodeAsync(userId, claimCode);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found with the provided claim code");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Order has already been processed");
            }

            order.Status = OrderStatus.Completed;

            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return MapToOrderDto(updatedOrder);
        }

        private string GenerateClaimCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public async Task<int> GetOrderCount()
        {
            return await _orderRepository.GetCompletedOrderCountAsync();
        }
        private OrderDto MapToOrderDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                DiscountPercentage = order.DiscountPercentage,
                TotalAmount = order.TotalAmount,
                UsedBulkDiscount = order.UsedBulkDiscount,
                UsedLoyaltyDiscount = order.UsedLoyaltyDiscount,
                ClaimCode = order.ClaimCode,
                CancellationDate = order.CancellationDate,
                UserId = order.UserId,
                UserFullName = order.User?.FullName ?? string.Empty,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderId = oi.OrderId,
                    BookId = oi.BookId,
                    BookTitle = oi.Book?.Title ?? string.Empty,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    LineTotal = oi.Quantity * oi.UnitPrice
                }).ToList()
            };
        }
    }
}