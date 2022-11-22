using AutoMapper;
using FluentResults;
using ProductManager.Domain;
using ProductManager.Application;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using static ProductManager.Domain.ProductCategoryState;
using System;

namespace ProductManager.Infrastructure
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCategoryRepository> _logger;
        private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

        public ProductCategoryRepository(IDbContextFactory<ProductDbContext> dbContextFactory, IMapper mapper, ILogger<ProductCategoryRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> CreateAsync(ProductCategoryState category)
        {
            try
            {
                var dataRow = _mapper.Map<ProductCategoryDataRow>(category);

                using var dbContext = _dbContextFactory.CreateDbContext();

                dbContext.ProductCategories.Add(dataRow);

                await dbContext.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Unable to create the product category. Error: {exception.Message}";

                _logger.LogError(exception, errorMessage);
                return Result.Fail(new Error(errorMessage).CausedBy(exception));
            }
        }

        public async Task<Result> DeleteAsync(ProductCategoryState category)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var dataRow = _mapper.Map<ProductCategoryDataRow>(category);

            var row = await dbContext.ProductCategories.FirstOrDefaultAsync(category => category.Id == dataRow.Id);

            if (row == null)
            {
                return Result.Ok();
            }
            else
            {
                dbContext.ProductCategories.Remove(row);

                await dbContext.SaveChangesAsync();

                return Result.Ok();
            }
        }

        public async Task<Result<ProductCategoryState>> GetByIdAsync(Guid categoryId)
        {
            try
            {
                using var dbContext = _dbContextFactory.CreateDbContext();

                var row = await dbContext.ProductCategories.FirstOrDefaultAsync(category => category.Id == categoryId);

                if(row == null)
                {
                    return Result.Ok(Empty);
                }
                else
                {
                    var state = _mapper.Map<ExistingProductCategory>(row);

                    return Result.Ok(state as ProductCategoryState);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = $"Unable to get the product category with ID: {categoryId}. Error: {exception.Message}";

                _logger.LogError(exception, errorMessage);
                return Result.Fail(new Error(errorMessage).CausedBy(exception));
            }
        }

        public async Task<Result<IEnumerable<ProductCategoryState>>> ListAsync(int offset, int limit)
        {
            try
            {
                using var dbContext = _dbContextFactory.CreateDbContext();

                var query = dbContext.ProductCategories
                                        .OrderBy(category => category.Id)
                                        .Skip(offset)
                                        .Take(limit)
                                        .ToArrayAsync();

                var queryResult = await query;

                var categories = queryResult.Select(category => _mapper.Map<ExistingProductCategory>(category) as ProductCategoryState);

                return Result.Ok(categories);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Unable to get the product categories. Error: {exception.Message}";

                _logger.LogError(exception, errorMessage);
                return Result.Fail(new Error(errorMessage).CausedBy(exception));
            }
        }

        public async Task<Result> UpdateAsync(ProductCategoryState category)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var dataRow = _mapper.Map<ProductCategoryDataRow>(category);

            var row = await dbContext.ProductCategories.FirstOrDefaultAsync(category => category.Id == dataRow.Id);

            if (row == null)
            {
                var errorMessage = $"Unable to update the product categories with ID : '{dataRow.Id}' as it's not found on the database";

                _logger.LogError(errorMessage);
                return Result.Fail(errorMessage);
            }
            else
            {
                dbContext.ProductCategories.Remove(row);

                await dbContext.SaveChangesAsync();
            }

            dbContext.ProductCategories.Add(dataRow);

            await dbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}