using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using TechLife.Data;
using TechLife.Models;
using TechLife.Models.DTOs;

namespace TechLife.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int shopId, int qty)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ShopStoreId == shopId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var book = _db.ShopStores.Find(shopId);
                    cartItem = new CartDetail
                    {
                        ShopStoreId = shopId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        SubTotal = book.price  // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(int shopId)
        {
            string userId = GetUserId();
            try
            {             
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("user is not logged in");
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("Invalid Cart");
                }
                //cart detail selection
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ShopStoreId == shopId);
              if(cartItem is null )
                {
                    throw new Exception("No items in cart");
                }
                else if(cartItem.Quantity == 1)
                {
                    _db.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }
        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if(userId == null)
            {
                throw new Exception("Invalid UserId");
            }
            var shoppingCart = await _db.ShoppingCarts.Include(a => a.CartDetails).ThenInclude(a => a.ShopStore).ThenInclude(a => a.Genre).Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;
        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
           return cart;
        }
        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from ShoppingCart in _db.ShoppingCarts
                              join CartDetail in _db.CartDetails
                              on ShoppingCart.Id equals CartDetail.ShoppingCartId
                              where ShoppingCart.UserId == userId
                              select new { CartDetail.Id }
                              ).ToListAsync();
            return data.Count;
        }
        public async Task<bool> DoCheckout(CheckoutModel model)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                //move data from cartDetail to ordr and order detail
                var userId = GetUserId();
                if(string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not loged in");
                }
                var cart = await GetCart(userId);
                if(cart is null)
                {
                    throw new Exception("Invalid Cart");
                }
                var cartDetail = _db.CartDetails.Where(a=>a.ShoppingCartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                {
                    throw new Exception("Cart Is Empty");
                }
                var pendingRecord = _db.OrderStatuses.FirstOrDefault(s => s.StatusName == "Pending");
                if(pendingRecord is null)
                {
                    throw new Exception("Order status does not have Pending Status");
                }
                var order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.UtcNow,
                    Name = model.Name,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    PaymentMethod = model.PaymentMethod,
                    Address = model.Address,
                    IsPaid = false,
                    OrderStatusId = pendingRecord.Id 

                };
                _db.Orders.Add(order);
                _db.SaveChanges();

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        ShopStoreId = item.ShopStoreId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.SubTotal
                    };
                    _db.OrderDetails.Add(orderDetail);
                }
                    _db.SaveChanges();

                    //removing Cart Detail
                    _db.CartDetails.RemoveRange(cartDetail);
                    _db.SaveChanges();
                    transaction.Commit();
                    return true;             
            }
            catch
            {
                return false;
            }
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
