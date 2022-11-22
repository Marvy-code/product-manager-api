using Npgsql;
using AutoMapper;
using ProductManager.Application;
using ProductManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManager.Composition
{
    public static class CompositionRoot
    {
        private static void ConfigureMapping(IServiceCollection services)
        {
            var config = new MapperConfiguration(config => {
                config.AddProfile<ProductCategoryMappingProfile>();
                config.AddProfile<ProductMappingProfile>();
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        private static void ConfigureDataAccess(IServiceCollection services)
        {
            var npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = DbConfig.PostGresServer,
                Database = DbConfig.PostGresDb,
                Username = DbConfig.PostGresUser,
                Password = DbConfig.PostGresPassword
            };

            var connectionString = npgsqlConnectionStringBuilder.ConnectionString;

            var dbContextOptions = new DbContextOptionsBuilder<ProductDbContext>().UseNpgsql(connectionString).Options;

            var productDbContextScopedFactory = new ProductDbContextScopedFactory(dbContextOptions);

            services.AddSingleton<IDbContextFactory<ProductDbContext>>(productDbContextScopedFactory);

            services.AddSingleton<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        public static void Compose(IServiceCollection services)
        {
            ConfigureMapping(services);
            ConfigureDataAccess(services);
        }
    }
}