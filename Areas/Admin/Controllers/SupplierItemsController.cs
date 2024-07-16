using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Repository;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierItemsController : Controller
    {
        private readonly ISupplierItemRepository _supplierItemRepo;
        private readonly ISupplierRepository _supplierRepo;
        private readonly IShopStoreRepository _shopStoreRepo;
        private readonly IStockRepository _stockRepo;

        public SupplierItemsController(ISupplierItemRepository supplierItemRepo, ISupplierRepository supplierRepo, IShopStoreRepository shopStoreRepo, IStockRepository stockRepo)
        {
            _supplierItemRepo = supplierItemRepo ?? throw new ArgumentNullException(nameof(supplierItemRepo));
            _supplierRepo = supplierRepo ?? throw new ArgumentNullException(nameof(supplierRepo));
            _shopStoreRepo = shopStoreRepo ?? throw new ArgumentNullException(nameof(shopStoreRepo));
            _stockRepo = stockRepo;
        }

        // GET: Admin/SupplierItems/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = await _supplierRepo.GetAllSuppliers();
            ViewBag.ShopStores = await _shopStoreRepo.GetShopStores();

            var model = new SupplierItem
            {
                PurchaseDate = DateTime.Today // Default to today's date
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierItem supplierItem)
        {
            // Assign Supplier and ShopStore navigation properties
            supplierItem.Supplier = await _supplierRepo.GetSupplierById(supplierItem.SupplierId);
            supplierItem.ShopStore = await _shopStoreRepo.GetByIdAsync((int)supplierItem.ShopStoreId);

            // Validate Supplier and ShopStore are assigned
            if (supplierItem.Supplier == null)
            {
                ModelState.AddModelError(nameof(SupplierItem.SupplierId), "Supplier is required.");
            }

            if (supplierItem.ShopStore == null)
            {
                ModelState.AddModelError(nameof(SupplierItem.ShopStoreId), "Shop Store is required.");
            }
            try
            {
                await _supplierItemRepo.AddSupplierItem(supplierItem);
                await _stockRepo.UpdateStockFromSupplierItem(supplierItem);
                return RedirectToAction("Index", "Suppliers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                ViewBag.Suppliers = await _supplierRepo.GetAllSuppliers();
                ViewBag.ShopStores = await _shopStoreRepo.GetShopStores();
                return View(supplierItem);
            }
        }


    }
}
