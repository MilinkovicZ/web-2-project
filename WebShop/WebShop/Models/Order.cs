﻿using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.Models
{
    public class Order : EntityBase
    {
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public OrderState OrderState { get; set; }
        public string? Comment { get; set; }
        public User? Buyer { get; set; }
        [Required]
        public int BuyerId { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
