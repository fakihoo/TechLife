using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TechLife.Data;
using TechLife.Models;

namespace TechLife.Repository
{
    public class ShopStoreRepository : IShopStoreRepository
    {
        private readonly ApplicationDbContext _db;
        public ShopStoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<IEnumerable<ShopStore>> GetShopStores(string sterm="", int genreId = 0)
        {
            sterm=sterm.ToLower();
            IEnumerable<ShopStore> ShopStores =  await (from ShopStore in _db.ShopStores
                              join genre in _db.Genres
                              on ShopStore.GenreId equals genre.Id
                              where string.IsNullOrWhiteSpace(sterm) || (ShopStore!=null && ShopStore.Name.ToLower().StartsWith(sterm))
                              select new ShopStore
                              {
                                  Id = ShopStore.Id,
                                  ImgUrl = ShopStore.ImgUrl,
                                  Name = ShopStore.Name,
                                  Description = ShopStore.Description,
                                  GenreId = ShopStore.GenreId,
                                  price = ShopStore.price,
                                  Quantity = ShopStore.Quantity,
                                  discount = ShopStore.discount,
                                  GenreName = ShopStore.GenreName

                              }

                              ).ToListAsync();
            if(genreId > 0)
            {
                ShopStores = ShopStores.Where(a => a.GenreId == genreId).ToList();
            }
            return ShopStores;
        }
    }
}
