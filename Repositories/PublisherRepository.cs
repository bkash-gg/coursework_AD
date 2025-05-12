using AD_Coursework.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using AD_Coursework.Models;
using AD_Coursework.Data;

namespace AD_Coursework.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(Guid id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Publishers.AnyAsync(p => p.Email == email);
        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
        {
            return await _context.Publishers.AnyAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> EmailExistsAsync(string email, Guid excludeId)
        {
            return await _context.Publishers.AnyAsync(p => p.Email == email && p.Id != excludeId);
        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid excludeId)
        {
            return await _context.Publishers.AnyAsync(p => p.PhoneNumber == phoneNumber && p.Id != excludeId);
        }

        public async Task<Publisher?> UpdatePublisherAsync(Publisher publisher)
        {
            var existingPublisher = await _context.Publishers.FindAsync(publisher.Id);
            if (existingPublisher == null) return null;

            _context.Entry(existingPublisher).CurrentValues.SetValues(publisher);
            await _context.SaveChangesAsync();
            return existingPublisher;
        }

        public async Task<bool> DeletePublisherAsync(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return false;

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PublisherExistsAsync(Guid id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }
    }
}
