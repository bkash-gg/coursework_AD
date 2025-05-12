using AD_Coursework.DTOs.Publisher;

namespace AD_Coursework.Interfaces.Services
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetAllAsync();
        Task<PublisherDto?> GetByIdAsync(Guid id);
        Task<PublisherDto> CreateAsync(PublisherCreateDto publisherCreateDto);
        Task<PublisherDto?> UpdateAsync(Guid id, PublisherUpdateDto publisherUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
