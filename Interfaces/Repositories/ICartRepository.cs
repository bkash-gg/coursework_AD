using AD_Coursework.Models;
using System.Threading.Tasks;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(Guid cartId);
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid bookId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(Guid cartId, Guid bookId);
        Task<bool> ClearCartAsync(Guid cartId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(Guid cartId);
        public Task<Cart?> GetCartByIdAsync(Guid cartId);
    }
}
