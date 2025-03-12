using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<OrderHeader>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Add(orderHeader);
            await _context.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<OrderDetails> AddOrderDetailsAsync(OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();
            return orderDetails;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync()
        {
            return await _context.OrderHeaders
                .Include(u => u.OrderDetails)
                .ThenInclude(u => u.Product)
                .OrderByDescending(u => u.OrderTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderHeader>> GetAllByUserIdAsync(string userId)
        {
            return await _context.OrderHeaders
                .Include(u => u.OrderDetails)
                .ThenInclude(u => u.Product)
                .OrderByDescending(u => u.OrderTime)
                .Where(u => u.UserId == userId)
                .ToListAsync();
        }

        public async Task<OrderHeader> GetByIdAsync(Guid orderHeaderId)
        {
            return await _context.OrderHeaders
                .Include(u => u.OrderDetails)
                .FirstOrDefaultAsync(u => u.Id == orderHeaderId);
        }

        public async Task<string?> UpdateStatusAsync(Guid orderHeaderId, string newStatus)
        {
            var orderHeader = await _context.OrderHeaders
                .FirstOrDefaultAsync(u => u.Id == orderHeaderId);

            if (orderHeader == null)
            {
                return null;
            }

            orderHeader.Status = newStatus;
            await _context.SaveChangesAsync();
            return newStatus;
        }

        public async Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader)
        {
            var existingOrder = await _context.OrderHeaders
                .FirstOrDefaultAsync(o => o.Id == orderHeader.Id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder = orderHeader;
            await _context.SaveChangesAsync();
            return existingOrder;
        }
    }
}
