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
            order.DeliveryTime = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(59));

            foreach (var item in order.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                    throw new NotFoundException("Product is non existent.");

                if (item.ProductAmount > product.Amount)
                    throw new BadRequestException("Can't buy more product then available.");

                if (item.ProductAmount < 0)
                    throw new BadRequestException("Can't buy negative number of products.");

                product.Amount -= item.ProductAmount;
                item.ProductId = product.Id;

                _unitOfWork.ProductsRepository.Update(product);
                _unitOfWork.ItemsRepository.Update(item);
            }

            await _unitOfWork.OrdersRepository.Insert(order);
            await _unitOfWork.Save();
        }

        public async Task DeclineOrder(int orderId, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            Order? order = await _unitOfWork.OrdersRepository.Get(orderId);
            if (order == null)
                throw new NotFoundException("Order doesn't exist within this user.");

            order.OrderState = OrderState.Canceled;

            foreach (var item in order.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                    throw new NotFoundException("Product is non existent.");

                product.Amount += item.ProductAmount;
                _unitOfWork.ProductsRepository.Update(product);
            }

            _unitOfWork.OrdersRepository.Update(order);
            await _unitOfWork.Save();
        }
        public async Task<List<OrderDTOWithTime>> GetMyOrders(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Include(x => x.Items).ThenInclude(x => x.Product).ToList();
            var buyerOrders = includedOrders.Where(x => x.BuyerId == buyerId && (x.OrderState == OrderState.Preparing || x.OrderState == OrderState.Delievered)).ToList();
            foreach (Order order in buyerOrders)
            {
                if (order.TimeToDeliver.CompareTo(TimeSpan.Zero) < 0)
                {
                    order.OrderState = OrderState.Delievered;
                    order.TimeToDeliver = TimeSpan.Zero;
                    _unitOfWork.OrdersRepository.Update(order);
                    await _unitOfWork.Save();
                }
            }
            return _mapper.Map<List<OrderDTOWithTime>>(buyerOrders);
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

        public async Task OrderDeliever(int orderId)
        {
            Order? order = await _unitOfWork.OrdersRepository.Get(orderId);
            if (order == null)
                throw new NotFoundException($"Unable to find order with ID: {orderId}.");
            if (order.OrderState == OrderState.Canceled)
                throw new BadRequestException($"Order with ID: {orderId} is already canceled.");

            if (order.DeliveryTime <= DateTime.UtcNow)
                order.OrderState = OrderState.Delievered;
            else
                order.OrderState = OrderState.Preparing;

            _unitOfWork.OrdersRepository.Update(order);
            await _unitOfWork.Save();
        }
    }
}
