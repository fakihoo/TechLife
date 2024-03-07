using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface ISimServiceRepository : IRepository<SimService>
    {
        void Update(SimService SimService);

    }
}
