using System.Linq.Expressions;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Service obj)
        {
            _db.Services.Update(obj);
        }
    }
}