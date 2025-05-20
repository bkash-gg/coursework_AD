using AD_Coursework.DTOs.Checkout;
using System;
using System.Threading.Tasks;

namespace AD_Coursework.Interfaces.Services
{
    public interface ICheckoutService
    {
        Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(Guid userId);
    }
}