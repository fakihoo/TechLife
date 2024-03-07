using System.Linq.Expressions;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class ToDoListRepository : Repository<ToDoList>, IToDoListRepository 
    {
        private ApplicationDbContext _db;
        public ToDoListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ToDoList obj)
        {
            _db.ToDoLists.Update(obj);
        }
    }
}
