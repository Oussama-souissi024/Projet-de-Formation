using E_Commerce.Application.Cart.DTOs;
using E_Commerce.Application.Orders.DTOs;
using System.Security.Claims;

namespace E_Commerce.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<OrderHeaderDto?> CreateOrderAsync(CartDto cartDto);


        // Retrieves all orders for a user based on their claims
        // For admin users, returns all orders
        // For regular users, returns only their orders
        Task<IEnumerable<OrderHeaderDto?>> GetAllOrderAsync(ClaimsPrincipal claimUser);

        Task<OrderHeaderDto> GetOrderByIdAsync(Guid orderHeaderId);

        Task<string?> UpdateOrderStatusAsync(Guid orderHeaderId, string newStatus);

    }
}
