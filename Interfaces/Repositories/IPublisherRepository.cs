using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber);
        Task<Publisher> CreatePublisherAsync(Publisher publisher);
        Task<bool> EmailExistsAsync(string email, Guid excludeId);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid excludeId);
        Task<Publisher?> UpdatePublisherAsync(Publisher publisher);
        Task<bool> DeletePublisherAsync(Guid id);
        Task<bool> PublisherExistsAsync(Guid id);
    }
}
