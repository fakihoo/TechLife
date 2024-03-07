using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public interface IUnitOfWork
    {
        IShopRepository Shop { get; }
        ICartRepository Cart { get; }
        IServiceRepository Service { get; }
        IToDoListRepository ToDoList { get; }
        ISimServiceRepository SimService { get; }
        ISimServicesToDoRepository SimServicesToDo { get; }
        void Save();
    }
}
