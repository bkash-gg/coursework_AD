using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetAllAsync();
        Task<Discount?> GetByIdAsync(Guid id);
        Task<Discount> CreateAsync(Discount discount);
        Task<Discount?> UpdateAsync(Discount discount);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Discount>> GetCurrentDiscountsAsync();
        Task<Discount?> GetDiscountByBookIdAsync(Guid bookId);
        Task<bool> DiscountExistsAsync(Guid id);
    }
}