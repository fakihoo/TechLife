using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;
using TechLife.EmailServices;
using TechLife.Models.DTOs;
using TechLife.Repository;
using TechLife.Utility;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepo;
        private readonly StripeService _stripeService;
        private readonly string _webhookSecret;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(IShoppingCartRepository cartRepo, StripeService stripeService, IConfiguration configuration, ILogger<ShoppingCartController> logger)
        {
            _cartRepo = cartRepo;
            _stripeService = stripeService;
            _webhookSecret = configuration["Stripe:WebhookSecret"];
            _logger = logger;
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
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.PaymentMethod == "Online")
            {
                var cart = await _cartRepo.GetUserCart();
                var lineItems = new List<Stripe.Checkout.SessionLineItemOptions>();

                foreach (var item in cart.CartDetails)
                {
                    lineItems.Add(new Stripe.Checkout.SessionLineItemOptions
                    {
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.SubTotal * 100), // amount in cents
                            Currency = "usd",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ShopStore.Name
                            }
                        },
                        Quantity = item.Quantity
                    });
                }

                var stripeSessionUrl = await _stripeService.CreateCheckoutSessionAsync(model.Email, model.Name, model.Address, lineItems);

                // Store checkout model in TempData
                TempData["CheckoutModel"] = JsonSerializer.Serialize(model);

                // Redirect to Stripe checkout
                return Redirect(stripeSessionUrl);
            }

            // Handle other payment methods
            bool isCheckedOut = await _cartRepo.DoCheckout(model);

            if (!isCheckedOut)
            {
                return RedirectToAction(nameof(OrderFailure));
            }
            return RedirectToAction(nameof(OrderSuccessCOD));
        }
        [HttpGet]
        public async Task<IActionResult> OrderSuccess(string session_id)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                return RedirectToAction("OrderFailure");
            }

            try
            {
                // Fetch the session details from Stripe
                var sessionService = new Stripe.Checkout.SessionService();
                var session = await sessionService.GetAsync(session_id);

                if (session.PaymentStatus == "paid")
                {
                    // Retrieve the stored checkout model
                    var checkoutModelJson = TempData["CheckoutModel"]?.ToString();
                    if (checkoutModelJson == null)
                    {
                        throw new Exception("Checkout model not found.");
                    }

                    var model = JsonSerializer.Deserialize<CheckoutModel>(checkoutModelJson);

                    var checkoutResult = await _cartRepo.DoCheckout(model);

                    if (checkoutResult)
                    {
                        return View(); // return your success view
                    }
                    else
                    {
                        return RedirectToAction("OrderFailure");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during order success processing.");
            }

            return RedirectToAction("OrderFailure");
        }


        public IActionResult OrderFailure()
        {
            return View();
        }
        public IActionResult OrderSuccessCOD()
        {
            return View();
        }

    }
}
