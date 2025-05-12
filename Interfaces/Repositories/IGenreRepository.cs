using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(Guid id);
        Task<bool> GenreExistsAsync(Guid id);
        Task<bool> GenreNameExistsAsync(string name);
        Task<bool> GenreNameExistsAsync(string name, Guid excludeId);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task<Genre?> UpdateGenreAsync(Genre genre);
        Task<bool> DeleteGenreAsync(Guid id);
    }
}