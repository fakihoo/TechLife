using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _supplierRepo;
        private readonly ISupplierItemRepository _supplierItemRepo;
        private readonly ApplicationDbContext _context;
        public SuppliersController(ISupplierRepository supplierRepo, ISupplierItemRepository supplierItemRepo, ApplicationDbContext context)
        {
            _supplierRepo = supplierRepo;
            _supplierItemRepo = supplierItemRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepo.GetAllSuppliers();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(supplier);
            }

            try
            {
                await _supplierRepo.AddSupplier(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(supplier);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            // Retrieve all supplier items for the specified supplier
            var supplierItems = await _supplierItemRepo.GetSupplierItemsBySupplierId(id);

            ViewBag.SupplierName = supplier.Name; // Optional: Pass supplier name to view if needed

            return View(supplierItems);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepo.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            try
            {
                // Get all related SupplierItems
                var supplierItems = await _supplierItemRepo.GetSupplierItemsBySupplierId(id);

                foreach (var item in supplierItems)
                {
                    // Detach Stocks from SupplierItem
                    var stocks = await _context.Stocks.Where(s => s.SupplierItemId == item.SupplierItemId).ToListAsync();
                    foreach (var stock in stocks)
                    {
                        stock.SupplierItemId = null; // Detach the stock from the supplier item
                    }

                    // Update stocks in the database
                    _context.Stocks.UpdateRange(stocks);
                }

                await _context.SaveChangesAsync();

                // Delete related SupplierItems
                foreach (var item in supplierItems)
                {
                    await _supplierItemRepo.DeleteSupplierItem(item.SupplierItemId);
                }

                // Delete the supplier
                await _supplierRepo.DeleteSupplier(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View("Index", await _supplierRepo.GetAllSuppliers());
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierRepo.GetSupplierById((int)id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,Name,Contact,Email,Phone")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supplierRepo.UpdateSupplier(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SupplierExists(supplier.SupplierId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        private async Task<bool> SupplierExists(int id)
        {
            return await _supplierRepo.GetSupplierById(id) != null;
        }
    }





}

