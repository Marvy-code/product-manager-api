namespace ProductManager.Application
{
    public record CreateProductCommand(Guid Id, Guid CategoryId, string Name, string? Description, decimal Price, int Quantity) : IRequest<Result<ProductDto>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<ProductDto>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var getStateResult = await _productRepository.GetByIdAsync(command.Id);

            if (getStateResult.IsSuccess)
            {
                //var state = getStateResult.Value;
                //var createResult = ProductAggregate.Create(state, command.Id, command.Name, command.Description, command.Price, command.Quantity);

                //if(createResult.IsSuccess)
                //{

                //}
            }
            else
            {

            }

            //result.IsSuccess switch
            //{
            //    true => result.Value switch
            //    {
            //        ExistingProduct => {
            //            Result.Ok(new ExistingProduct(id, name, description) as ProductState)
            //        },
            //        _ => (Result<ProductState>)Result.Fail($"We already have a product product with ID '{id}'"),
            //    }
            //    false =>
            //}

            //await _productRepository.CreateAsync(product);

            return Result.Ok(new ProductDto(command.Id, command.CategoryId, command.Name, command.Description, command.Price, command.Quantity));
        }
    }
}