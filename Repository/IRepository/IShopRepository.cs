using System.Linq.Expressions;
using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface IShopRepository : IRepository<Shop>
    {
        Task<IEnumerable<Shop>> FindByConditionAsync(Expression<Func<Shop, bool>> expression);
        void Update(Shop shop);

    }
}
