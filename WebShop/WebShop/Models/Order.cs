using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Order : EntityBase
    {
        public List<Item> Items { get; set; } = new List<Item>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        [Required]
        public DateTime DeliveryTime { get; set; }
        public string? Comment { get; set; }
    }
}
