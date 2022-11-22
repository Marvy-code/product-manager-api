using ProductManager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Api
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductInput input)
        {
            var command = new CreateProductCommand(Guid.NewGuid(), input.CategoryId, input.Name, input.Description, input.Price, input.Quantity);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Created("", result.Value);
            }
            else
            {
                return Problem(statusCode: 500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> List([FromQuery] int offset, [FromQuery] int limit)
        {
            var query = new ListProductsQuery(offset, limit);

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return Problem(statusCode: 500);
            }
        }
    }
}