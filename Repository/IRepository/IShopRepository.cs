using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface IShopRepository : IRepository<Shop>
    {
        void Update(Shop shop);

    }
}
