using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public async Task<IEnumerable<Shop>> FindByConditionAsync(Expression<Func<Shop, bool>> expression)
        {
            return await _db.Shops.Where(expression).ToListAsync();
        }
    }
}
