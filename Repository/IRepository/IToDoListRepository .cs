using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface IToDoListRepository : IRepository<ToDoList>
    {
        void Update(ToDoList toDoList);

    }
}
