using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Order;
using AD_Coursework.DTOs.OrderItem;
using AD_Coursework.Models;
using AD_Coursework.Utils;

namespace AD_Coursework.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IEmailService _emailService;

        public OrderService(
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository,
            ICartRepository cartRepository,
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _emailService = emailService;
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            return MapToOrderDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapToOrderDto).ToList();
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(orderCreateDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            decimal subtotal = 0;
            var orderItems = new List<OrderItem>();

            foreach (var itemDto in orderCreateDto.OrderItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(itemDto.BookId);
                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {itemDto.BookId} not found");
                }

                if (!book.IsAvailable || book.StockQuantity < itemDto.Quantity)
                {
                    throw new InvalidOperationException($"Book '{book.Title}' is not available in the requested quantity");
                }

                var lineTotal = book.Price * itemDto.Quantity;
                subtotal += lineTotal;

                orderItems.Add(new OrderItem
                {
                    BookId = book.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = book.Price,
                    LineTotal = lineTotal
                });
            }

            decimal discountAmount = 0;
            decimal discountPercentage = 0;
            bool usedBulkDiscount = false;
            bool usedLoyaltyDiscount = false;

            var totalBooks = orderCreateDto.OrderItems.Sum(oi => oi.Quantity);
            if (totalBooks >= 5)
            {
                discountPercentage += 5;
                usedBulkDiscount = true;
            }

            var completedOrdersCount = await _orderRepository.GetUserOrderCountAsync(orderCreateDto.UserId);
            if (completedOrdersCount >= 10 && user.IsEligibleForLoyaltyDiscount)
            {
                discountPercentage += 10;
                usedLoyaltyDiscount = true;
            }

            if (discountPercentage > 0)
            {
                discountAmount = subtotal * (discountPercentage / 100);
            }

            var totalAmount = subtotal - discountAmount;

            var order = new Order
            {
                UserId = orderCreateDto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Subtotal = subtotal,
                DiscountAmount = discountAmount,
                DiscountPercentage = discountPercentage,
                TotalAmount = totalAmount,
                UsedBulkDiscount = usedBulkDiscount,
                UsedLoyaltyDiscount = usedLoyaltyDiscount,
                ClaimCode = GenerateClaimCode(),
                PickupNotes = orderCreateDto.PickupNotes,
                PickupDate = orderCreateDto.PickupDate,
                PaymentMethod = orderCreateDto.PaymentMethod,
                OrderItems = orderItems
            };

            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            foreach (var item in orderCreateDto.OrderItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(item.BookId);
                if (book != null)
                {
                    book.StockQuantity -= item.Quantity;
                    await _bookRepository.UpdateBookAsync(book);
                }
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(orderCreateDto.UserId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(cart.Id);
            }

            if (usedLoyaltyDiscount)
            {
                user.IsEligibleForLoyaltyDiscount = false;
                await _userRepository.UpdateUserAsync(user);
            }

            await _emailService.SendOrderConfirmationEmail(user.Email, createdOrder);

            return MapToOrderDto(createdOrder);
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            if (!string.IsNullOrEmpty(orderUpdateDto.Status))
            {
                if (Enum.TryParse<OrderStatus>(orderUpdateDto.Status, out var status))
                {
                    order.Status = status;
                }
            }

            order.PickupDate = orderUpdateDto.PickupDate;
            order.PickupNotes = orderUpdateDto.PickupNotes;
            order.PaymentMethod = orderUpdateDto.PaymentMethod;

            if (order.Status == OrderStatus.Cancelled && order.CancellationDate == null)
            {
                order.CancellationDate = DateTime.UtcNow;
                order.CancellationReason = orderUpdateDto.CancellationReason;

                foreach (var item in order.OrderItems)
                {
                    var book = await _bookRepository.GetBookByIdAsync(item.BookId);
                    if (book != null)
                    {
                        book.StockQuantity += item.Quantity;
                        await _bookRepository.UpdateBookAsync(book);
                    }
                }
            }

            if (order.Status == OrderStatus.Completed && order.CompletionDate == null)
            {
                order.CompletionDate = DateTime.UtcNow;

                var user = await _userRepository.GetUserByIdAsync(order.UserId);
                if (user != null)
                {
                    user.TotalOrdersCompleted++;

                    if (user.TotalOrdersCompleted % 10 == 0)
                    {
                        user.IsEligibleForLoyaltyDiscount = true;
                    }

                    await _userRepository.UpdateUserAsync(user);
                }
            }

            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return MapToOrderDto(updatedOrder);
        }

        public async Task<bool> CancelOrderAsync(Guid orderId, string cancellationReason)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;

            if (order.Status == OrderStatus.Completed)
            {
                throw new InvalidOperationException("Completed orders cannot be cancelled");
            }

            var updateDto = new OrderUpdateDto
            {
                Status = OrderStatus.Cancelled.ToString(),
                CancellationReason = cancellationReason
            };

            await UpdateOrderAsync(orderId, updateDto);
            return true;
        }

        private string GenerateClaimCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
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
                PickupNotes = order.PickupNotes,
                PickupDate = order.PickupDate,
                PaymentMethod = order.PaymentMethod,
                CancellationDate = order.CancellationDate,
                CancellationReason = order.CancellationReason,
                CompletionDate = order.CompletionDate,
                UserId = order.UserId,
                UserFullName = order.User?.FullName ?? string.Empty,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderId = oi.OrderId,
                    BookId = oi.BookId,
                    BookTitle = oi.Book?.Title ?? string.Empty,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    LineTotal = oi.LineTotal
                }).ToList()
            };
        }
    }
}