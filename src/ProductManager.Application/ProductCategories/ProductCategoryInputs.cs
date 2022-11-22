namespace ProductManager.Application
{
    public class CreateProductCategoryInput
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class RenameProductCategoryInput
    {
        public string NewName { get; set; }
    }
}