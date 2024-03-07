using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Update(Cart shop);

    }
}