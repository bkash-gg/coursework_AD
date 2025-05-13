using AD_Coursework.DTOs.Discount;

namespace AD_Coursework.Interfaces.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDto>> GetAllAsync();
        Task<DiscountDto?> GetByIdAsync(Guid id);
        Task<DiscountDto> CreateAsync(DiscountCreateDto discountCreateDto);
        Task<DiscountDto?> UpdateAsync(Guid id, DiscountUpdateDto discountUpdateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<DiscountDto>> GetCurrentDiscountsAsync();
        Task<DiscountDto?> GetDiscountByBookIdAsync(Guid bookId);
    }
}