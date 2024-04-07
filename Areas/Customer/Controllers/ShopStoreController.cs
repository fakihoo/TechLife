using Microsoft.AspNetCore.Mvc;
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
    }
}
