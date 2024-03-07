using System.Linq.Expressions;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class SimServicesToDoRepository : Repository<SimServicesToDo>, ISimServicesToDoRepository
    {
        private ApplicationDbContext _db;
        public SimServicesToDoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(SimServicesToDo obj)
        {
            _db.SimServiceToDos.Update(obj);
        }
    }
}
