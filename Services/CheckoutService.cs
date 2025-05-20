using AD_Coursework.DTOs.Checkout;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace AD_Coursework.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public CheckoutService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(Guid userId)
        {
            try
            {
                var summary = await _orderRepository.GetCheckoutSummaryAsync(userId);
                return summary;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Unable to generate checkout summary: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating checkout summary", ex);
            }
        }
    }
}