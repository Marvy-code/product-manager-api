using Microsoft.AspNetCore.Mvc;

// https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-7.0

namespace ProductManager.Api
{
    [ApiController]
    [Route("v1/categories")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(IMediator mediator, ILogger<ProductCategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> Create(CreateProductCategoryInput input)
        {
            _logger.LogInformation($"[API:CREATE_PRODUCT_CATEGORY] Name: {input.Name}, Descritption: {input.Description}");

            var command = new CreateProductCategoryCommand(Guid.NewGuid(), input.Name, input.Description);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Created("", result.Value);
            }
            else
            {
                var errors = result.Errors.Select(error => error.Message).Aggregate((partialPhrase, message) => $"{partialPhrase}\n{message}");

                return Problem(statusCode: 500, detail: errors);
            }
        }

        [HttpPut("{categoryId}/rename")]
        public async Task<ActionResult<ProductCategoryDto>> Rename([FromRoute] Guid categoryId, RenameProductCategoryInput input)
        {
            _logger.LogInformation($"[API:RENAME_PRODUCT_CATEGORY] Id: {categoryId}, NewName: {input.NewName}");

            var command = new RenameProductCategoryCommand(categoryId, input.NewName);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                var errors = result.Errors.Select(error => error.Message).Aggregate((partialPhrase, message) => $"{partialPhrase}\n{message}");

                return Problem(statusCode: 500, detail: errors);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProductCategoryDto>> List([FromQuery] int offset, [FromQuery] int limit)
        {
            _logger.LogInformation($"[API:LIST_PRODUCT_CATEGORIES] Offset: {offset}, Limit: {limit}");

            var query = new ListProductCategoriesQuery(offset, limit);

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                var errors = result.Errors.Select(error => error.Message).Aggregate((partialPhrase, message) => $"{partialPhrase}\n{message}");

                return Problem(statusCode: 500, detail: errors);
            }
        }
    }
}