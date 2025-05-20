using AD_Coursework.DTOs.WhitelistItem;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class WhitelistItemService : IWhitelistItemService
    {
        private readonly IWhitelistItemRepository _whitelistItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public WhitelistItemService(
            IWhitelistItemRepository whitelistItemRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository)
        {
            _whitelistItemRepository = whitelistItemRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<WhitelistItemDto>> GetWhitelistItemsByUserIdAsync(Guid userId)
        {
            var whitelistItems = await _whitelistItemRepository.GetWhitelistItemsByUserIdAsync(userId);

            var result = whitelistItems.Select(item => new WhitelistItemDto
            {
                Id = item.Id,
                UserId = item.UserId,
                BookId = item.BookId,
                AddedOn = item.AddedOn,
                BookTitle = item.Book?.Title,
                BookImageURL = item.Book?.CoverImageUrl,
                AuthorName = item.Book?.Author?.Name
            });

            return result;
        }

        public async Task<WhitelistItemDto?> GetWhitelistItemByIdAsync(Guid id)
        {
            var whitelistItem = await _whitelistItemRepository.GetWhitelistItemByIdAsync(id);

            if (whitelistItem == null)
                return null;

            return new WhitelistItemDto
            {
                Id = whitelistItem.Id,
                UserId = whitelistItem.UserId,
                BookId = whitelistItem.BookId,
                AddedOn = whitelistItem.AddedOn
            };
        }

        public async Task<WhitelistItemDto> CreateWhitelistItemAsync(WhitelistItemCreateDto whitelistItemCreateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(whitelistItemCreateDto.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var book = await _bookRepository.GetBookByIdAsync(whitelistItemCreateDto.BookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }

            var alreadyExists = await _whitelistItemRepository.WhitelistItemExistsAsync(
                whitelistItemCreateDto.UserId,
                whitelistItemCreateDto.BookId);

            if (alreadyExists)
            {
                throw new InvalidOperationException("This book is already in your whitelist.");
            }

            var whitelistItem = new WhitelistItem
            {
                UserId = whitelistItemCreateDto.UserId,
                BookId = whitelistItemCreateDto.BookId,
                User = user,      
                Book = book,     
                AddedOn = DateTime.UtcNow
            };

            var createdWhitelistItem = await _whitelistItemRepository.CreateWhitelistItemAsync(whitelistItem);

            return new WhitelistItemDto
            {
                Id = createdWhitelistItem.Id,
                UserId = createdWhitelistItem.UserId,
                BookId = createdWhitelistItem.BookId,
                AddedOn = createdWhitelistItem.AddedOn
            };
        }

        public async Task<bool> DeleteWhitelistItemAsync(Guid id)
        {
            return await _whitelistItemRepository.DeleteWhitelistItemAsync(id);
        }

        public async Task<bool> WhitelistItemExistsAsync(Guid userId, Guid bookId)
        {
            return await _whitelistItemRepository.WhitelistItemExistsAsync(userId, bookId);
        }

        public async Task<int> GetWhitelistItemCountForUserAsync(Guid userId)
        {
            return await _whitelistItemRepository.GetWhitelistItemCountForUserAsync(userId);
        }
    }
}