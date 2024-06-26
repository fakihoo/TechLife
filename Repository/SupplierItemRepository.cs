using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models;

namespace TechLife.Repository
{
    public class SupplierItemRepository : ISupplierItemRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierItem>> GetAllSupplierItems()
        {
            return await _context.SupplierItems
                .Include(si => si.Supplier)
                .Include(si => si.ShopStore)
                .ToListAsync();
        }

        public async Task AddSupplierItem(SupplierItem supplierItem)
        {
            _context.SupplierItems.Add(supplierItem);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public IEnumerable<ShopStore> GetShopStores()
        {
            return _context.ShopStores.ToList();
        }
        public async Task<IEnumerable<SupplierItem>> GetSupplierItemsBySupplierId(int supplierId)
        {
            return await _context.SupplierItems
                                 .Where(si => si.SupplierId == supplierId)
                                 .ToListAsync();
        }

        public async Task DeleteSupplierItem(int supplierItemId)
        {
            var item = await _context.SupplierItems.FindAsync(supplierItemId);
            if (item != null)
            {
                _context.SupplierItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }



    }
}
