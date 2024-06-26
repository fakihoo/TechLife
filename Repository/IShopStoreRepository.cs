using TechLife.Models;

namespace TechLife
{
    public interface IShopStoreRepository
    {
        Task<IEnumerable<ShopStore>> GetShopStores(string sterm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
        Task<ShopStore> GetByIdAsync(int id);
        Task UpdateDiscountAsync(int id, double discount);
        Task<bool> ExistsAsync(int id);


    }
}