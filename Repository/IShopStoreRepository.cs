using TechLife.Models;

namespace TechLife
{
    public interface IShopStoreRepository
    {
        Task<IEnumerable<ShopStore>> GetShopStores(string sterm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();


    }
}