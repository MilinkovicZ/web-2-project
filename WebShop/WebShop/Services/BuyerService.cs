using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class BuyerService : IBuyerService
    {
        private double DeliveryFee {get; set;} = 2.99;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BuyerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateOrder(CreateOrderDTO orderDTO, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var order = _mapper.Map<Order>(orderDTO);
            order.BuyerId = buyerId;
            order.OrderState = OrderState.Preparing;
            order.StartTime = DateTime.Now;
            order.DeliveryTime = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(59));

            double totalPrice = 0;
            var sellerIds = new List<int>();

            foreach (var item in order.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                    throw new NotFoundException("Product is non existent.");

                if (item.ProductAmount > product.Amount)
                    throw new BadRequestException($"Currently there is only {product.Amount} {product.Name}s in stock.");

                if (item.ProductAmount < 0)
                    throw new BadRequestException("Can't buy negative number of products.");


                product.Amount -= item.ProductAmount;
                item.Name = product.Name;
                item.CurrentPrice = product.Price;

                totalPrice += item.ProductAmount * item.CurrentPrice;
                if (!sellerIds.Contains(product.SellerId)) 
                    sellerIds.Add(product.SellerId);

                _unitOfWork.ProductsRepository.Update(product);
            }

            totalPrice += sellerIds.Count() * DeliveryFee;
            order.TotalPrice = totalPrice;

            await _unitOfWork.OrdersRepository.Insert(order);
            await _unitOfWork.Save();
        }

        public async Task DeclineOrder(int orderId, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrder = orders.Include(x => x.Items);
            var myOrder = includedOrder.FirstOrDefault(x => x.Id == orderId);
            if (myOrder == null)
                throw new NotFoundException("Order doesn't exist within this user.");
            if (myOrder.OrderState == OrderState.Delievered)
                throw new BadRequestException("Can't cancel already delivered order.");
            if (myOrder.StartTime.AddHours(1) < DateTime.Now)
                throw new BadRequestException("1 hour already passed, you can't cancel your order.");

            myOrder.OrderState = OrderState.Canceled;

            foreach (var item in myOrder.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                    throw new NotFoundException("Product is non existent.");

                product.Amount += item.ProductAmount;
                _unitOfWork.ProductsRepository.Update(product);
            }

            _unitOfWork.OrdersRepository.Update(myOrder);
            await _unitOfWork.Save();
        }
        public async Task<List<OrderDTO>> GetMyOrders(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Include(x => x.Items).ToList();
            var buyerOrders = includedOrders.Where(x => x.BuyerId == buyerId && (x.OrderState == OrderState.Preparing || x.OrderState == OrderState.Delievered)).ToList();
            
            return _mapper.Map<List<OrderDTO>>(buyerOrders);
        }

        public async Task<List<ProductDTO>> GetAllProducts(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var products = await _unitOfWork.ProductsRepository.GetAll();
            List<Product> availableProduct = products.Where(x => x.Amount > 0).ToList();
            return _mapper.Map<List<ProductDTO>>(availableProduct);
        }
    }
}
