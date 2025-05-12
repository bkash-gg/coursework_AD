using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Genre;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllGenresAsync();
            return genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });
        }

        public async Task<GenreDto?> GetByIdAsync(Guid id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null) return null;

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };
        }

        public async Task<GenreDto> CreateAsync(GenreCreateDto genreCreateDto)
        {
            if (string.IsNullOrWhiteSpace(genreCreateDto.Name))
            {
                throw new ArgumentException("Genre name is required.");
            }

            if (await _genreRepository.GenreNameExistsAsync(genreCreateDto.Name))
            {
                throw new InvalidOperationException("A genre with this name already exists.");
            }

            var genre = new Genre
            {
                Name = genreCreateDto.Name,
                Description = genreCreateDto.Description
            };

            var createdGenre = await _genreRepository.CreateGenreAsync(genre);

            return new GenreDto
            {
                Id = createdGenre.Id,
                Name = createdGenre.Name,
                Description = createdGenre.Description
            };
        }

        public async Task<GenreDto?> UpdateAsync(Guid id, GenreUpdateDto genreUpdateDto)
        {
            var existingGenre = await _genreRepository.GetGenreByIdAsync(id);
            if (existingGenre == null) return null;

            if (existingGenre.Name != genreUpdateDto.Name &&
                await _genreRepository.GenreNameExistsAsync(genreUpdateDto.Name, id))
            {
                throw new InvalidOperationException("A genre with this name already exists.");
            }

            existingGenre.Name = genreUpdateDto.Name;
            existingGenre.Description = genreUpdateDto.Description;

            var updatedGenre = await _genreRepository.UpdateGenreAsync(existingGenre);
            if (updatedGenre == null) return null;

            return new GenreDto
            {
                Id = updatedGenre.Id,
                Name = updatedGenre.Name,
                Description = updatedGenre.Description
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _genreRepository.DeleteGenreAsync(id);
        }
    }
}