using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Cart;
using AD_Coursework.DTOs.CartItem;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;

        public CartService(ICartRepository cartRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
        }

        public async Task<CartDto> GetCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                var newCart = new Cart { UserId = userId };
                cart = await _cartRepository.CreateCartAsync(newCart);
            }

            return MapToCartDto(cart);
        }

        public async Task<CartDto> AddToCartAsync(Guid userId, AddToCartDto addToCartDto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId) ??
                await _cartRepository.CreateCartAsync(new Cart { UserId = userId });

            var book = await _bookRepository.GetBookByIdAsync(addToCartDto.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is not available for purchase");
            }

            var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, addToCartDto.BookId);

            if (existingItem != null)
            {
                existingItem.Quantity += addToCartDto.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    Cart = cart, 
                    BookId = addToCartDto.BookId,
                    Book = book, 
                    Quantity = addToCartDto.Quantity,
                    UnitPrice = book.Price
                };
                await _cartRepository.AddCartItemAsync(newItem);
            }

            await UpdateCartTotals(cart.Id);

            return await GetCartAsync(userId);
        }

        public async Task<CartDto> UpdateCartItemAsync(Guid userId, CartItemUpdateDto updateDto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }

            var cartItem = await _cartRepository.GetCartItemAsync(cart.Id, updateDto.BookId);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("Item not found in cart");
            }

            cartItem.Quantity = updateDto.Quantity;
            await _cartRepository.UpdateCartItemAsync(cartItem);

            await UpdateCartTotals(cart.Id);

            return await GetCartAsync(userId);
        }

        public async Task<CartDto> RemoveFromCartAsync(Guid userId, RemoveFromCartDto removeDto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }

            var success = await _cartRepository.RemoveCartItemAsync(cart.Id, removeDto.BookId);
            if (!success)
            {
                throw new KeyNotFoundException("Item not found in cart");
            }

            await UpdateCartTotals(cart.Id);

            return await GetCartAsync(userId);
        }

        public async Task<bool> ClearCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            return await _cartRepository.ClearCartAsync(cart.Id);
        }

        private async Task UpdateCartTotals(Guid cartId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(cartId);
            if (cart == null) return;

            var items = await _cartRepository.GetCartItemsAsync(cartId);

            cart.ItemCount = items.Sum(i => i.Quantity);
            cart.Subtotal = items.Sum(i => i.Quantity * i.UnitPrice);

            await _cartRepository.UpdateCartAsync(cart);
        }

        private CartDto MapToCartDto(Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                ItemCount = cart.ItemCount,
                Subtotal = cart.Subtotal,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    CartId = i.CartId,
                    BookId = i.BookId,
                    BookTitle = i.Book?.Title ?? string.Empty,
                    BookCoverImageUrl = i.Book?.CoverImageUrl ?? string.Empty,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.Quantity * i.UnitPrice
                }).ToList()
            };
        }
    }
}
