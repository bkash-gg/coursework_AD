using AD_Coursework.DTOs.Order;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;
using System.Net;
using System.Net.Mail;

namespace AD_Coursework.Services.Utilities
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            var emailConfig = _configuration.GetSection("EmailSettings");

            _smtpHost = emailConfig["SmtpHost"]!;
            _smtpPort = int.Parse(emailConfig["SmtpPort"]!);
            _smtpUsername = emailConfig["SmtpUsername"]!;
            _smtpPassword = emailConfig["SmtpPassword"]!;
            _fromEmail = emailConfig["FromEmail"]!;
            _fromName = emailConfig["FromName"]!;
        }

        public async Task SendOrderConfirmationEmail(string email, Order order)
        {
            var subject = $"Order Confirmation - #{order.Id.ToString().Substring(0, 8).ToUpper()}";

            var emailBody = $@"
                <html>
                <body>
                    <h1>Order Confirmation</h1>
                    <p>Dear Valued Customer,</p>
                    <p>Thank you for your order with our bookstore!</p>
                    
                    <h2>Order Summary</h2>
                    <table border='1' cellpadding='5' cellspacing='0'>
                        <tr>
                            <th>Order ID</th>
                            <td>{order.Id}</td>
                        </tr>
                        <tr>
                            <th>Order Date</th>
                            <td>{order.OrderDate:yyyy-MM-dd HH:mm}</td>
                        </tr>
                        <tr>
                            <th>Claim Code</th>
                            <td><strong>{order.ClaimCode}</strong></td>
                        </tr>
                        <tr>
                            <th>Subtotal</th>
                            <td>{order.Subtotal:C}</td>
                        </tr>
                        <tr>
                            <th>Discount</th>
                            <td>-{order.DiscountAmount:C} ({order.DiscountPercentage}%)</td>
                        </tr>
                        <tr>
                            <th>Total Amount</th>
                            <td><strong>{order.TotalAmount:C}</strong></td>
                        </tr>
                    </table>
                    
                    <h2>Order Items</h2>
                    <table border='1' cellpadding='5' cellspacing='0'>
                        <tr>
                            <th>Book</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>Total</th>
                        </tr>
                        {string.Join("", order.OrderItems.Select(oi => $@"
                        <tr>
                            <td>{oi.Book?.Title}</td>
                            <td>{oi.Quantity}</td>
                            <td>{oi.UnitPrice:C}</td>
                            <td>{oi.LineTotal:C}</td>
                        </tr>"))}
                    </table>
                    
                    <h2>Pickup Information</h2>
                    <p>Please bring this claim code and your membership ID to the store to pick up your order.</p>
                    <p><strong>Claim Code:</strong> {order.ClaimCode}</p>
                    
                    <p>If you have any questions, please contact our customer service.</p>
                    <p>Thank you for shopping with us!</p>
                    <p>Best regards,<br/>{_fromName}</p>
                </body>
                </html>
            ";

            await SendEmailAsync(email, subject, emailBody, true);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_fromEmail, _fromName);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isBodyHtml;

                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                    await client.SendMailAsync(message);
                }
            }
        }
    }
}