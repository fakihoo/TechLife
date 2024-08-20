using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IShopRepository Shop { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IToDoListRepository ToDoList { get; private set; }
        public ISimServiceRepository SimService { get; private set; }
        public ISimServicesToDoRepository SimServicesToDo { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Shop = new ShopRepository(_db);
            Cart = new CartRepository(_db);
            Service = new ServiceRepository(_db);
            ToDoList = new ToDoListRepository(_db);
            SimService = new SimServiceRepository(_db);
            SimServicesToDo = new SimServicesToDoRepository(_db);
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
