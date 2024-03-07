using System.Linq.Expressions;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class SimServiceRepository : Repository<SimService>, ISimServiceRepository 
    {
        private ApplicationDbContext _db;
        public SimServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(SimService obj)
        {
            _db.SimServices.Update(obj);
        }
    }
}
