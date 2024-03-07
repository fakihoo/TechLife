using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface ISimServicesToDoRepository : IRepository<SimServicesToDo>
    {
        void Update(SimServicesToDo SimServicesToDo);

    }
}
