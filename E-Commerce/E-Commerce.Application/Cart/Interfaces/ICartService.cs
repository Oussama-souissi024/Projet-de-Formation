using E_Commerce.Application.Cart.DTOs;

namespace E_Commerce.Application.Cart.Interfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartByUserIdAsync(string userId);
        Task<string?> UpsertCartAsync(CartDto cartDto);
        Task<string?> ApplyCouponAsync(CartDto cartDto);
        Task<string?> RemoveFromCartAsync(Guid cartDetailsId);
        Task<bool> ClearCartAsync(string userId);
    }
}
