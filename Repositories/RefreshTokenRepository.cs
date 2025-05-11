using AD_Coursework.Data;
using AD_Coursework.Models;
using AD_Coursework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .ToListAsync();
        }

        public async Task<RefreshToken?> GetActiveTokenByUserIdAsync(Guid userId)
        {
            return await _dbContext.RefreshTokens
                .Where(rt => rt.UserId == userId &&
                       !rt.IsRevoked &&
                       !rt.IsUsed &&
                       rt.ExpiryDate > DateTime.UtcNow)
                .OrderByDescending(rt => rt.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task RevokeAllUserTokensAsync(Guid userId)
        {
            var userTokens = await _dbContext.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();

            foreach (var token in userTokens)
            {
                token.IsRevoked = true;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}