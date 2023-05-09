using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Item : EntityBase
    {
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public int ProductAmount { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
    }
}
