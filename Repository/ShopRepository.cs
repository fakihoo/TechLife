using System.Linq.Expressions;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class ShopRepository : Repository<Shop>, IShopRepository 
    {
        private ApplicationDbContext _db;
        public ShopRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Shop shop)
        {
            _db.Shops.Update(shop);
        }
    }
}
