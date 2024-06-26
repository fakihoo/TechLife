using TechLife.Models;

namespace TechLife.Repository
{
    public interface ISupplierItemRepository
    {
        Task<IEnumerable<SupplierItem>> GetAllSupplierItems();
        Task AddSupplierItem(SupplierItem supplierItem);
        IEnumerable<Supplier> GetSuppliers();
        IEnumerable<ShopStore> GetShopStores();
        Task<IEnumerable<SupplierItem>> GetSupplierItemsBySupplierId(int supplierId);
        Task DeleteSupplierItem(int supplierItemId);
    }
}
