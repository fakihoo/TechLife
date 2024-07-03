using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository.IRepository;

namespace TechLife.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Images).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Images)
                .FirstOrDefault(p => p.Id == id);
        }


        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            // EF Core tracks changes, so no need for additional logic here
            _context.Products.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _context.Products.Find(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
