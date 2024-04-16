using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLife.Repository;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepo;
        public ShoppingCartController(IShoppingCartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int shopId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(shopId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem(int ShopId)
        {
            var cartCount = await _cartRepo.RemoveItem(ShopId);
            return RedirectToAction("GetUserCart");

        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }
        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _cartRepo.DoCheckout();
            if (!isCheckedOut)
            {
                throw new Exception("Something Happened in server side");
            }
            return RedirectToAction("Index", "ShopStore");

        }

    }
}
