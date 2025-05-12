using AD_Coursework.DTOs.Genre;

namespace AD_Coursework.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
        Task<GenreDto?> GetByIdAsync(Guid id);
        Task<GenreDto> CreateAsync(GenreCreateDto genreCreateDto);
        Task<GenreDto?> UpdateAsync(Guid id, GenreUpdateDto genreUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}