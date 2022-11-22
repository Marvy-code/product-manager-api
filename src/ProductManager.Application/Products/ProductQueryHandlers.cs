using AutoMapper;

namespace ProductManager.Application
{
    public record ListProductsQuery(int Offset, int Limit) : IRequest<Result<IEnumerable<ProductDto>>>;

    public class ListProductQueryHandler : IRequestHandler<ListProductsQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ListProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(ListProductsQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAsync(query.Offset, query.Limit);

            if(result.IsSuccess)
            {
                var categories = result.Value.Select(_mapper.Map<ProductDto>);

                return Result.Ok(categories);
            }
            else
            {
                return Result.Fail(result.Errors);
            }
        }
    }
}