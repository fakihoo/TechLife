using TechLife.Models;

namespace TechLife.Repository
{
    public interface IShoppingCartRepository
    {
        Task<int> AddItem(int shopId, int qty);
        Task<int> RemoveItem(int shopStoreId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");

        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout();


    }
}