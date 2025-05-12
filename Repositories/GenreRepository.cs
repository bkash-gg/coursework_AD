using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(Guid id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<bool> GenreExistsAsync(Guid id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> GenreNameExistsAsync(string name)
        {
            return await _context.Genres.AnyAsync(g => g.Name == name);
        }

        public async Task<bool> GenreNameExistsAsync(string name, Guid excludeId)
        {
            return await _context.Genres.AnyAsync(g => g.Name == name && g.Id != excludeId);
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre?> UpdateGenreAsync(Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(genre.Id);
            if (existingGenre == null) return null;

            _context.Entry(existingGenre).CurrentValues.SetValues(genre);
            await _context.SaveChangesAsync();
            return existingGenre;
        }

        public async Task<bool> DeleteGenreAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}