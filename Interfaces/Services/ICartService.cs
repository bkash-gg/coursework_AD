using AD_Coursework.DTOs.Cart;
using AD_Coursework.DTOs.CartItem;
using System.Threading.Tasks;

namespace AD_Coursework.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(Guid userId);
        Task<CartDto> AddToCartAsync(Guid userId, AddToCartDto addToCartDto);
        Task<CartDto> UpdateCartItemAsync(Guid userId, CartItemUpdateDto updateDto);
        Task<CartDto> RemoveFromCartAsync(Guid userId, RemoveFromCartDto removeDto);
        Task<bool> ClearCartAsync(Guid userId);
    }
}
