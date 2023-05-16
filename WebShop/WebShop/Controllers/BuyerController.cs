﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpGet("Products")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _buyerService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("Orders")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetMyOrders()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int buyerId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            var orders = await _buyerService.GetMyOrders(buyerId);
            return Ok(orders);
        }

        [HttpPost("CreateOrder")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int buyerId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _buyerService.CreateOrder(orderDTO, buyerId);
            return Ok();
        }

        [HttpPost("DeclineOrder/{id}")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> DeclineOrder(int orderId)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int buyerId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _buyerService.DeclineOrder(orderId, buyerId);
            return Ok();
        }
    }
}
