using Microsoft.EntityFrameworkCore;

namespace ProductManager.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductDataRow> Products { get; set; }
        public DbSet<ProductCategoryDataRow> ProductCategories { get; set; }
    }

    public class ProductDbContextScopedFactory : IDbContextFactory<ProductDbContext>
    {
        public readonly DbContextOptions<ProductDbContext> _dbContextOptions;

        public ProductDbContextScopedFactory(DbContextOptions<ProductDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public ProductDbContext CreateDbContext()
        {
            var dbContext = new ProductDbContext(_dbContextOptions);

            return dbContext;
        }
    }
}
