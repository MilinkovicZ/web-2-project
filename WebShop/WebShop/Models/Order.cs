using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.Models
{
    public class Order : EntityBase
    {
        public List<Item> Items { get; set; } = new List<Item>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public OrderState OrderState { get; set; }
        public string? Comment { get; set; }
        public User? Buyer { get; set; }
        public int BuyerId { get; set; }
    }
}
