using FluentResults;
using static ProductManager.Domain.ProductCategoryState;

namespace ProductManager.Domain
{
    public abstract record ProductCategoryState
    {
        public record EmptyProductCategory() : ProductCategoryState;

        public record ExistingProductCategory(
            Guid Id,
            string Name,
            string? Description
        ) : ProductCategoryState;

        public static ProductCategoryState Empty { get; } = new EmptyProductCategory();

        private ProductCategoryState() { }
    }

    public class ProductCategoryAggregate
    {
        public static Result<ProductCategoryState> Create(ProductCategoryState currentState, Guid id, string name, string? description)
        {
            return currentState switch
            {
                EmptyProductCategory => Result.Ok(new ExistingProductCategory(id, name, description) as ProductCategoryState),
                _ => (Result<ProductCategoryState>)Result.Fail($"We already have a product category with ID '{id}'"),
            };
        }

        public static Result<ProductCategoryState> Rename(ProductCategoryState currentState, string newName)
        {
            return currentState switch
            {
                ExistingProductCategory state =>Result.Ok(new ExistingProductCategory(state.Id, newName, state.Description) as ProductCategoryState),
                _ => (Result<ProductCategoryState>)Result.Fail($"Unable to rename the product category as it's not exits"),
            };
        }
    }
}