using Microsoft.EntityFrameworkCore;
using MvcMovie1.Data;
using MvcMovie2.Features.Products.Models;

namespace MvcMovie2.Features.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly MvcMovie1Context _db;

        public ProductService(MvcMovie1Context db) => _db = db;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Product.ToListAsync();
        

        public async Task<Product?> GetByIdAsync(int id) => await _db.Product.FirstOrDefaultAsync(product => product.Id == id);


        public async Task AddAsync(Product product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _db.Product.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Product.FindAsync(id);
            if (entity is null) return;          
                _db.Product.Remove(entity);
                await _db.SaveChangesAsync();
            
        }

    }
}
