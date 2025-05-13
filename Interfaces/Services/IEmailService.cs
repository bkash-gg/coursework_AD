using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendOrderConfirmationEmail(string email, Order order);
    }
}