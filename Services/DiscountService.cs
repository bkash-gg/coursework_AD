using AD_Coursework.DTOs.Discount;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IBookRepository _bookRepository;

        public DiscountService(IDiscountRepository discountRepository, IBookRepository bookRepository)
        {
            _discountRepository = discountRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<DiscountDto>> GetAllAsync()
        {
            var discounts = await _discountRepository.GetAllAsync();
            return discounts.Select(d => new DiscountDto
            {
                Id = d.Id,
                DiscountPercentage = d.DiscountPercentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                BookId = d.BookId,
                BookTitle = d.Book?.Title ?? string.Empty,
                ISBN = d.Book?.ISBN ?? string.Empty
            });
        }

        public async Task<DiscountDto?> GetByIdAsync(Guid id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            if (discount == null) return null;

            return new DiscountDto
            {
                Id = discount.Id,
                DiscountPercentage = discount.DiscountPercentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                BookId = discount.BookId,
                BookTitle = discount.Book?.Title ?? string.Empty,
                ISBN = discount.Book?.ISBN ?? string.Empty
            };
        }

        public async Task<DiscountDto> CreateAsync(DiscountCreateDto discountCreateDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(discountCreateDto.BookId);
            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID.");
            }

            var discount = new Discount
            {
                DiscountPercentage = discountCreateDto.DiscountPercentage,
                StartDate = DateTime.SpecifyKind(discountCreateDto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(discountCreateDto.EndDate, DateTimeKind.Utc),
                BookId = discountCreateDto.BookId
            };

            var createdDiscount = await _discountRepository.CreateAsync(discount);
            return new DiscountDto
            {
                Id = createdDiscount.Id,
                DiscountPercentage = createdDiscount.DiscountPercentage,
                StartDate = createdDiscount.StartDate,
                EndDate = createdDiscount.EndDate,
                BookId = createdDiscount.BookId,
                BookTitle = book.Title,
                ISBN = book.ISBN
            };
        }

        public async Task<DiscountDto?> UpdateAsync(Guid id, DiscountUpdateDto discountUpdateDto)
        {
            var existingDiscount = await _discountRepository.GetByIdAsync(id);
            if (existingDiscount == null) return null;

            existingDiscount.DiscountPercentage = discountUpdateDto.DiscountPercentage;
            existingDiscount.StartDate = DateTime.SpecifyKind(discountUpdateDto.StartDate, DateTimeKind.Utc);
            existingDiscount.EndDate = DateTime.SpecifyKind(discountUpdateDto.EndDate, DateTimeKind.Utc);

            var updatedDiscount = await _discountRepository.UpdateAsync(existingDiscount);
            return new DiscountDto
            {
                Id = updatedDiscount.Id,
                DiscountPercentage = updatedDiscount.DiscountPercentage,
                StartDate = updatedDiscount.StartDate,
                EndDate = updatedDiscount.EndDate,
                BookId = updatedDiscount.BookId,
                BookTitle = updatedDiscount.Book?.Title ?? string.Empty,
                ISBN = updatedDiscount.Book?.ISBN ?? string.Empty
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _discountRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DiscountDto>> GetCurrentDiscountsAsync()
        {
            var discounts = await _discountRepository.GetCurrentDiscountsAsync();
            return discounts.Select(d => new DiscountDto
            {
                Id = d.Id,
                DiscountPercentage = d.DiscountPercentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                BookId = d.BookId,
                BookTitle = d.Book?.Title ?? string.Empty,
                ISBN = d.Book?.ISBN ?? string.Empty
            });
        }

        public async Task<DiscountDto?> GetDiscountByBookIdAsync(Guid bookId)
        {
            var discount = await _discountRepository.GetDiscountByBookIdAsync(bookId);
            if (discount == null) return null;

            return new DiscountDto
            {
                Id = discount.Id,
                DiscountPercentage = discount.DiscountPercentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                BookId = discount.BookId,
                BookTitle = discount.Book?.Title ?? string.Empty,
                ISBN = discount.Book?.ISBN ?? string.Empty
            };
        }
    }
}