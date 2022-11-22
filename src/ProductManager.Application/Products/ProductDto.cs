namespace ProductManager.Application
{
    public record ProductDto(Guid Id, Guid CategoryId, string Name, string? Description, decimal Price, int Quantity);
}