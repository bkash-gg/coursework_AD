using AD_Coursework.Data;
using AD_Coursework.Models;
using AD_Coursework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetByIdWithRoleAsync(Guid id)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetPaginatedUsersAsync(int page, int pageSize)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .OrderBy(u => u.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _dbContext.Users.CountAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
        {
            if (excludeUserId.HasValue)
            {
                return !await _dbContext.Users.AnyAsync(u =>
                    u.Email == email && u.Id != excludeUserId.Value);
            }

            return !await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username, Guid? excludeUserId = null)
        {
            if (excludeUserId.HasValue)
            {
                return !await _dbContext.Users.AnyAsync(u =>
                    u.UserName == username && u.Id != excludeUserId.Value);
            }

            return !await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task UpdateLoyaltyStatusAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                // Example loyalty logic: If completed orders >= 5, enable loyalty discount
                user.IsEligibleForLoyaltyDiscount = user.TotalOrdersCompleted >= 5;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}