using TechLife.Models;

namespace TechLife.Repository
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task AddSupplier(Supplier supplier);
        Task<Supplier> GetSupplierById(int? id);
        Task DeleteSupplier(int id);
        Task UpdateSupplier(Supplier supplier);

    }
}
