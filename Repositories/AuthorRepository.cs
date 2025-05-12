using AD_Coursework.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using AD_Coursework.Models;
using AD_Coursework.Data;

namespace AD_Coursework.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Authors.AnyAsync(a => a.Email == email);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> EmailExistsAsync(string email, Guid excludeId)
        {
            return await _context.Authors.AnyAsync(a => a.Email == email && a.Id != excludeId);
        }

        public async Task<Author?> UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(author.Id);
            if (existingAuthor == null) return null;

            _context.Entry(existingAuthor).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AuthorExistsAsync(Guid id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
    }
}
