using FluentResults;
using static ProductManager.Domain.ProductState;

namespace ProductManager.Domain
{
    public abstract record ProductState
    {
        public record EmptyProduct() : ProductState;

        public record ExistingProduct(
            Guid Id,
            Guid CategoryId,
            string Name,
            string? Description,
            decimal Price,
            int Quantity
        ) : ProductState;

        public static ProductState Empty { get; } = new EmptyProduct();

        private ProductState() { }
    }

    public class ProductAggregate
    {
        public static Result<ProductState> Create(ProductState currentState, ProductCategoryState categoryState, Guid id, Guid categoryId, string name, string? description, decimal price, int quantity)
        {
            return currentState switch
            {
                ExistingProduct => Result.Ok(new ExistingProduct(id, categoryId, name, description, price, quantity) as ProductState),
                _ => (Result<ProductState>)Result.Fail($"We already have a product  with ID '{id}'"),
            };
        }
    }
}