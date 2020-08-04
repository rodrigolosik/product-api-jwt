using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> ListProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
