using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models;

namespace TechLife.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }
        public async Task<Supplier> GetSupplierById(int? id)
        {
            if (id == null)
                return null;

            return await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);
        }
        public async Task DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateSupplier(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
