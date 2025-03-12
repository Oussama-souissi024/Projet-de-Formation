using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Interfaces.Repositories.Base;
using System.Security.Claims;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<OrderHeader>
    {
        Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader);
        Task<OrderDetails> AddOrderDetailsAsync(OrderDetails orderDetails);
        Task<IEnumerable<OrderHeader>> GetAllAsync();
        Task<IEnumerable<OrderHeader>> GetAllByUserIdAsync(string UserId);
        Task<OrderHeader> GetByIdAsync(Guid orderHeaderId);
        Task<string?> UpdateStatusAsync(Guid orderHeaderId, string newStatus);
        Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader);

    }
}
