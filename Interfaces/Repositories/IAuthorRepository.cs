using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task<Author> CreateAuthorAsync(Author author);
        Task<bool> EmailExistsAsync(string email, Guid excludeId);
        Task<Author?> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(Guid id);
        Task<bool> AuthorExistsAsync(Guid id);
    }
}
