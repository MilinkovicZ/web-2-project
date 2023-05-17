using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
        public string? Description { get; set; }
    }

    public class ProductDTO : CreateProductDTO
    {
        [Required]
        public int Id { get; set; }
        public byte[]? Image { get; set; }
    }
}
