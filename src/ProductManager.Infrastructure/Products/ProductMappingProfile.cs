using AutoMapper;
using ProductManager.Application;
using static ProductManager.Domain.ProductState;

namespace ProductManager.Infrastructure
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ExistingProduct, ProductDataRow>();
            CreateMap<ProductDataRow, ExistingProduct>();
            CreateMap<ExistingProduct, ProductDto>();
        }
    }
}
