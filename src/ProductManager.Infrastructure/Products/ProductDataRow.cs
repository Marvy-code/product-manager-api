using System.ComponentModel.DataAnnotations;

namespace ProductManager.Infrastructure
{
    public class ProductDataRow
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ProductCategoryDataRow Category { get; set; }
    }
}
