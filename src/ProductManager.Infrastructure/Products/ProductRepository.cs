using AutoMapper;
using ProductManager.Domain;
using ProductManager.Application;
using Microsoft.EntityFrameworkCore;
using FluentResults;
using static ProductManager.Domain.ProductState;

namespace ProductManager.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ProductRepository(IDbContextFactory<ProductDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(ProductState product)
        {
            try
            {
                var dataRow = _mapper.Map<ProductDataRow>(product);

                using var dbContext = _dbContextFactory.CreateDbContext();

                dbContext.Products.Add(dataRow);

                await dbContext.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Task<Result> DeleteAsync(ProductState product)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ProductState>> GetByIdAsync(Guid productId)
        {
            try
            {
                using var dbContext = _dbContextFactory.CreateDbContext();

                var row = await dbContext.Products.FirstOrDefaultAsync(product => product.Id == productId);

                if(row == null)
                {
                    return Result.Ok(Empty);
                }
                else
                {
                    var state = _mapper.Map<ExistingProduct>(row);

                    return Result.Ok(state as ProductState);
                }
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public async Task<Result<IEnumerable<ProductState>>> ListAsync(int offset, int limit)
        {
            try
            {
                using var dbContext = _dbContextFactory.CreateDbContext();

                var query = dbContext.Products
                                        .OrderBy(product => product.Id)
                                        .Skip(offset)
                                        .Take(limit)
                                        .ToArrayAsync();

                var queryResult = await query;

                var categories = queryResult.Select(product => _mapper.Map<ExistingProduct>(product) as ProductState);

                return Result.Ok(categories);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message);
            }
        }

        public Task<Result> UpdateAsync(ProductState product)
        {
            throw new NotImplementedException();
        }
    }
}