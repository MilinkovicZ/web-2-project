using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class ItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductAmount { get; set; }
    }
}
