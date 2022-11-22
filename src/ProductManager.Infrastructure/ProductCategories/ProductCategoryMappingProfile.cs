using AutoMapper;
using ProductManager.Application;
using static ProductManager.Domain.ProductCategoryState;

namespace ProductManager.Infrastructure
{
    public class ProductCategoryMappingProfile : Profile
    {
        public ProductCategoryMappingProfile()
        {
            CreateMap<ExistingProductCategory, ProductCategoryDataRow>();
            CreateMap<ProductCategoryDataRow, ExistingProductCategory>();
            CreateMap<ExistingProductCategory, ProductCategoryDto>();
        }
    }
}
