using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Item : EntityBase
    {
        [Required]
        public Product? Product { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductAmount { get; set; }
        [Required]
        public double CurrentPrice { get; set; }
        public Order? Order { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
