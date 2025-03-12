using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class CartRepository : Repository<CartHeader>, ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId)
        {
            return await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(ch => ch.UserId == userId);
        }
        public async Task<IEnumerable<CartDetails>> GetListCartDetailsByCartHeaderIdAsync(Guid CartHeaderId)
        {
            return await _context.CartDetails
                       .Where(cd => cd.CartHeaderId == CartHeaderId)
                       .ToListAsync();
        }
        public async Task<CartHeader> AddCartHeaderAsync(CartHeader cartHeader)
        {
            _context.CartHeaders.Add(cartHeader);
            await _context.SaveChangesAsync();
            return cartHeader;
        }
        public async Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails)
        {
            _context.CartDetails.Add(cartDetails);
            await _context.SaveChangesAsync();
            return cartDetails;
        }
        public async Task<CartDetails?> GetCartDetailsByCartHeaderIdAndProductId(Guid cartHeaderId, Guid productId)
        {
            return await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        c => c.ProductId == productId && c.CartHeaderId == cartHeaderId);
        }
        public async Task<CartHeader?> UpdateCartHeaderAsync(CartHeader cartHeader)
        {
            _context.CartHeaders.Update(cartHeader);
            await _context.SaveChangesAsync();
            return cartHeader;
        }
        public async Task<CartDetails> UpdateCartDetailsAsync(CartDetails cartDetails)
        {
            _context.CartDetails.Update(cartDetails);
            await _context.SaveChangesAsync();
            return cartDetails;
        }
        public async Task<CartHeader?> GetCartHeaderByCartHeaderId(Guid cartHeaderId)
        {
            return await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(ch => ch.Id == cartHeaderId);
        }
        public async Task<CartDetails?> GetCartDetailsByCartDetailsId(Guid cartDetailsId)
        {
            return await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(ch => ch.Id == cartDetailsId);
        }
        public async Task<CartHeader?> RemoveCartHeaderAsync(CartHeader cartHeader)
        {
            _context.CartHeaders.Remove(cartHeader);
            await _context.SaveChangesAsync();
            return cartHeader;
        }
        public async Task<CartDetails?> RemoveCartDetailsAsync(CartDetails cartDetails)
        {
            _context.CartDetails.Remove(cartDetails);
            await _context.SaveChangesAsync();
            return cartDetails;
        }
        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartHeader = await _context.CartHeaders
                .Include(ch => ch.CartDetails)
                .FirstOrDefaultAsync(ch => ch.UserId == userId);

            if (cartHeader == null)
                return false;

            _context.CartDetails.RemoveRange(cartHeader.CartDetails);
            _context.CartHeaders.Remove(cartHeader);
            await _context.SaveChangesAsync();
            return true;
        }
        public int TotalCountofCartItem(Guid cartHeaderId)
        {
            return  _context.CartDetails.Where(cd => cd.CartHeaderId == cartHeaderId).Count();
        }
    }
}
