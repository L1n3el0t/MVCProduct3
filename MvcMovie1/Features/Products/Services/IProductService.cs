using System.Collections.Generic;
using System.Threading.Tasks;
using MvcMovie2.Features.Products.Models;

namespace MvcMovie2.Features.Products.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);
    }
}
