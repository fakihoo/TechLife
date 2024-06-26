using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLife.Models;
using TechLife.Models.DTOs;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopStoreController : Controller
    {
        private readonly IShopStoreRepository _shopStoreRepository;

        public ShopStoreController(IShopStoreRepository shopStoreRepository)
        {
            _shopStoreRepository = shopStoreRepository;
        }
        public async Task<IActionResult> Index(string sterm="", int genreId=0)
        {
            IEnumerable<ShopStore> shopStores = await _shopStoreRepository.GetShopStores(sterm,genreId);
            IEnumerable<Genre> Genres = await _shopStoreRepository.Genres();
            ShopDisplay ShopModel = new ShopDisplay
            {
                ShopStores = shopStores,
                Genres = Genres,
                STerm=sterm,
                GenreId = genreId
            };
            return View(ShopModel);
        }
        public async Task<IActionResult> EditDiscount(int id)
        {
            var shopStore = await _shopStoreRepository.GetByIdAsync(id);
            if (shopStore == null)
            {
                return NotFound();
            }

            var discountViewModel = new DiscountViewModel
            {
                Id = shopStore.Id,
                Discount = shopStore.discount
            };

            return View(discountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscount(DiscountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _shopStoreRepository.UpdateDiscountAsync(model.Id, model.Discount);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ShopStoreExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Debugging ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Console.WriteLine(error); // or use any logging mechanism
            }

            return View(model);
        }



        private async Task<bool> ShopStoreExists(int id)
        {
            return await _shopStoreRepository.ExistsAsync(id);
        }
    }
}
