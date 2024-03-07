using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service shop);

    }
}
