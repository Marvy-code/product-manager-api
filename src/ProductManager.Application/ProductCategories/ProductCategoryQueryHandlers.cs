using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ProductManager.Application
{
    public record ListProductCategoriesQuery(int Offset, int Limit) : IRequest<Result<IEnumerable<ProductCategoryDto>>>;

    public class ListProductCategoryQueryHandler : IRequestHandler<ListProductCategoriesQuery, Result<IEnumerable<ProductCategoryDto>>>
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListProductCategoriesQuery> _logger;

        public ListProductCategoryQueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogger<ListProductCategoriesQuery> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ProductCategoryDto>>> Handle(ListProductCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAsync(query.Offset, query.Limit);

            if(result.IsSuccess)
            {
                var categories = result.Value.Select(_mapper.Map<ProductCategoryDto>);

                return Result.Ok(categories);
            }
            else
            {
                return Result.Fail(result.Errors);
            }
        }
    }
}