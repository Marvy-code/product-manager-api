using FluentResults;
using ProductManager.Domain;

namespace ProductManager.Application
{
    public interface IProductRepository
    {
        Task<Result> CreateAsync(ProductState product);
        Task<Result> UpdateAsync(ProductState product);
        Task<Result> DeleteAsync(ProductState product);
        Task<Result<ProductState>> GetByIdAsync(Guid Id);
        Task<Result<IEnumerable<ProductState>>> ListAsync(int offset, int limit);
    }
}