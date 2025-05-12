using AD_Coursework.DTOs.Author;

namespace AD_Coursework.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(Guid id);
        Task<AuthorDto> CreateAsync(AuthorCreateDto authorCreateDto);
        Task<AuthorDto?> UpdateAsync(Guid id, AuthorUpdateDto authorUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
