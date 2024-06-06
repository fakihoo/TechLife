using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLife.Models.DTOs;
using TechLife.Repository;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockRepo.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManageStock(int shopStoreId)
        {
            var existingStock = await _stockRepo.GetStockByBookId(shopStoreId);
            var stock = new StockDTO
            {
                ShopStoreId = shopStoreId,
                Quantity = existingStock != null
            ? existingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
                return View(stock);
            try
            {
                await _stockRepo.ManageStock(stock);
                TempData["successMessage"] = "Stock is updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong!!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
