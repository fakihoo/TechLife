using System.Linq.Expressions;
using TechLife.Models;

namespace TechLife.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service shop);
        Task<IEnumerable<Service>> GetAllAsync(Expression<Func<Service, bool>> filter = null);
    }

}

