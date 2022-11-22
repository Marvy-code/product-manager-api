using FluentResults;
using ProductManager.Domain;

namespace ProductManager.Application
{
    public interface IProductCategoryRepository
    {
        Task<Result> CreateAsync(ProductCategoryState category);
        Task<Result> UpdateAsync(ProductCategoryState category);
        Task<Result> DeleteAsync(ProductCategoryState category);
        Task<Result<ProductCategoryState>> GetByIdAsync(Guid categoryId);
        Task<Result<IEnumerable<ProductCategoryState>>> ListAsync(int offset, int limit);
    }
}