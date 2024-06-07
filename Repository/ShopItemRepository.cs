using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models;

namespace TechLife.Repository
{
    public interface IShopItemRepository
    {
        Task AddShopItem(ShopStore shopStore);
        Task DeleteShopItem(ShopStore shopStore);
        Task<ShopStore?> GetShopItemById(int id);
        Task<IEnumerable<ShopStore>> GetShops();
        Task UpdateShopItem(ShopStore shopStore);
    }

    public class ShopItemRepository : IShopItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddShopItem(ShopStore shopStore)
        {
            _context.ShopStores.Add(shopStore);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShopItem(ShopStore shopStore)
        {
            _context.ShopStores.Update(shopStore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShopItem(ShopStore shopStore)
        {
            _context.ShopStores.Remove(shopStore);
            await _context.SaveChangesAsync();
        }

        public async Task<ShopStore?> GetShopItemById(int id) => await _context.ShopStores.FindAsync(id);

        public async Task<IEnumerable<ShopStore>> GetShops() => await _context.ShopStores.Include(a => a.Genre).ToListAsync();
    }
}
