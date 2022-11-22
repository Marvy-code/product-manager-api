using AutoMapper;
using ProductManager.Domain;

namespace ProductManager.Application
{
    public record CreateProductCategoryCommand(Guid Id, string Name, string? Description) : IRequest<Result<ProductCategoryDto>>;

    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Result<ProductCategoryDto>>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateProductCategoryCommandHandler(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductCategoryDto>> Handle(CreateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var getStateResult = await _categoryRepository.GetByIdAsync(command.Id);

            if (getStateResult.IsSuccess)
            {
                var state = getStateResult.Value;
                var createResult = ProductCategoryAggregate.Create(state, command.Id, command.Name, command.Description);

                if(createResult.IsSuccess)
                {
                    var createState = createResult.Value;

                    await _categoryRepository.CreateAsync(createState);

                    var dto = _mapper.Map<ProductCategoryDto>(createState);

                    return Result.Ok(dto);
                }
                else
                {
                    return Result.Fail(createResult.Errors);
                }
            }
            else
            {
                return Result.Fail(getStateResult.Errors);
            }
        }
    }

    public record RenameProductCategoryCommand(Guid Id, string NewName) : IRequest<Result<ProductCategoryDto>>;

    public class RenameProductCategoryCommandHandler : IRequestHandler<RenameProductCategoryCommand, Result<ProductCategoryDto>>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public RenameProductCategoryCommandHandler(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductCategoryDto>> Handle(RenameProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var getStateResult = await _categoryRepository.GetByIdAsync(command.Id);

            if (getStateResult.IsSuccess)
            {
                var getState = getStateResult.Value;
                var renameResult = ProductCategoryAggregate.Rename(getState, command.NewName);

                if (renameResult.IsSuccess)
                {
                    var renameState = renameResult.Value;

                    await _categoryRepository.UpdateAsync(renameState);

                    var dto = _mapper.Map<ProductCategoryDto>(renameState);

                    return Result.Ok(dto);
                }
                else
                {
                    return Result.Fail(renameResult.Errors);
                }
            }
            else
            {
                return Result.Fail(getStateResult.Errors);
            }
        }
    }
}