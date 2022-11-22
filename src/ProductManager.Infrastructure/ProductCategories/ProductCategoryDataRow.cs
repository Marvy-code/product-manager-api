using System.ComponentModel.DataAnnotations;

namespace ProductManager.Infrastructure
{
    public class ProductCategoryDataRow
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<ProductDataRow> Products { get; set; }
    }
}
