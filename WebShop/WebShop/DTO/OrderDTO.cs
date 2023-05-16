using System.ComponentModel.DataAnnotations;
namespace WebShop.DTO
{
    public class CreateOrderDTO
    {
        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public string? Comment { get; set; }
    }

    public class OrderDTO : CreateOrderDTO
    {
        [Required]
        public int Id { get; set; }
    }

    public class OrderDTOWithTime : OrderDTO
    {
        public TimeSpan TimeToDeliver { get; set;}
    }
}
