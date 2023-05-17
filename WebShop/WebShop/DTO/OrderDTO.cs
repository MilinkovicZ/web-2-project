using System.ComponentModel.DataAnnotations;
namespace WebShop.DTO
{
    public class CreateOrderDTO
    {
        public List<CreateItemDTO> Items { get; set; } = new List<CreateItemDTO>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public string? Comment { get; set; }
    }

    public class OrderDTO
    {
        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public string? Comment { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
