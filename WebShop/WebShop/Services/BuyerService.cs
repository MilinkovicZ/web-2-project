using AutoMapper;
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
            if (buyer == null || buyer.IsDeleted)
                throw new BadRequestException("Unable to find user with " + buyerId + ".");

            var order = _mapper.Map<Order>(orderDTO);
            order.BuyerId = buyerId;
            order.OrderState = OrderState.Preparing;
            order.DeliveryTime = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(59));

            foreach (var item in order.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null || product.IsDeleted)
                    throw new BadRequestException("Product is non existent.");

                if (item.ProductAmount > product.Amount)
                    throw new BadRequestException("Can't buy more product then available.");

                if (item.ProductAmount < 0)
                    throw new BadRequestException("Can't buy negative number of products.");

                product.Amount -= item.ProductAmount;
                _unitOfWork.ProductsRepository.Update(product);
            }

            await _unitOfWork.OrdersRepository.Insert(order);
            await _unitOfWork.Save();
        }

        public async Task DeclineOrder(int orderId, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if(buyer == null || buyer.IsDeleted)
                throw new BadRequestException("Unable to find user with " + buyerId + ".");

            Order? order = await _unitOfWork.OrdersRepository.Get(orderId);
            if (order == null || order.IsDeleted)
                throw new BadRequestException("Order doesn't exist within this user.");

            order.OrderState = OrderState.Canceled;
            _unitOfWork.OrdersRepository.Update(order);
            await _unitOfWork.Save();
        }
        public async Task<List<OrderDTO>> GetMyOrders(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null || buyer.IsDeleted)
                throw new BadRequestException("Unable to find user with " + buyerId + ".");

            var buyerOrders = await _unitOfWork.OrdersRepository.GetAll();
            List<Order> orders = buyerOrders.Where(x => x.BuyerId == buyerId && x.IsDeleted == false && (x.OrderState == OrderState.Preparing || x.OrderState == OrderState.Delievered)).ToList();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _unitOfWork.ProductsRepository.GetAll();
            List<Product> availableProduct = products.Where(x => !x.IsDeleted && x.Amount > 0).ToList();
            return _mapper.Map<List<ProductDTO>>(availableProduct);
        }
    }
}
