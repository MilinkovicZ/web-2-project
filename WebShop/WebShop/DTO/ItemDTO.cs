using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class CreateItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductAmount { get; set; }
    }

    public class ItemDTO : CreateItemDTO
    {
        public CreateProductDTO? Product { get; set; }
    }
}
