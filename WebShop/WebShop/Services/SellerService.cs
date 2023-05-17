using AutoMapper;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class SellerService : ISellerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SellerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateNewProduct(CreateProductDTO productDTO, int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            var product = _mapper.Map<Product>(productDTO);
            product.SellerId = sellerId;

            await _unitOfWork.ProductsRepository.Insert(product);
            await _unitOfWork.Save();
        }

        public async Task DeleteProduct(int productId, int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            Product? product = await _unitOfWork.ProductsRepository.Get(productId);
            if (product == null || product.SellerId != sellerId)
                throw new NotFoundException($"Unable to find product with ID: {productId}.");

            product.IsDeleted = true;
            _unitOfWork.ProductsRepository.Update(product);
            await _unitOfWork.Save();
        }

        public async Task UpdateProduct(int productId, CreateProductDTO productDTO, int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            Product? product = await _unitOfWork.ProductsRepository.Get(productId);
            if (product == null || product.SellerId != sellerId)
                throw new NotFoundException($"Unable to find product with ID: {productId}.");

            _mapper.Map(productDTO, product);

            _unitOfWork.ProductsRepository.Update(product);
            await _unitOfWork.Save();
        }

        public async Task<List<OrderDTO>> GetAllOrders(int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Where(o => o.OrderState == OrderState.Delievered).Include(o => o.Items).ThenInclude(i => i.Product);
            var sellerOrders = includedOrders.Where(o => o.Items.Any(i => i.Product!.SellerId == sellerId)).ToList();
            return _mapper.Map<List<OrderDTO>>(sellerOrders);
        }

        public async Task<List<OrderDTOWithTime>> GetNewOrders(int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Where(o => o.OrderState == OrderState.Preparing).Include(o => o.Items).ThenInclude(i => i.Product);
            var sellerOrders = includedOrders.Where(o => o.Items.Any(i => i.Product!.SellerId == sellerId)).ToList();
            foreach (Order order in sellerOrders)
            {
                order.TimeToDeliver = order.DeliveryTime - DateTime.Now;
                if(order.TimeToDeliver.CompareTo(TimeSpan.Zero) < 0)
                {
                    order.OrderState = OrderState.Delievered;
                    order.TimeToDeliver = TimeSpan.Zero;
                    _unitOfWork.OrdersRepository.Update(order);
                    await _unitOfWork.Save();
                }
            }
            return _mapper.Map<List<OrderDTOWithTime>>(sellerOrders);
        }

        public async Task<List<ProductDTO>> GetAllProducts(int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            var products = await _unitOfWork.ProductsRepository.GetAll();
            List<Product> sellerProducts = products.Where(x => x.SellerId == sellerId).ToList();
            return _mapper.Map<List<ProductDTO>>(sellerProducts);
        }

        public async Task AddProductPicture(int productId, IFormFile image, int sellerId)
        {
            User? seller = await _unitOfWork.UsersRepository.Get(sellerId);
            if (seller == null)
                throw new UnauthorizedException($"Unable to find user with ID: {sellerId}.");

            Product? product = await _unitOfWork.ProductsRepository.Get(productId);
            if (product == null || product.SellerId != sellerId)
                throw new NotFoundException($"Unable to find product with ID: {productId}.");

            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var imageBytes = ms.ToArray();

                product.Image = imageBytes;
                _unitOfWork.ProductsRepository.Update(product);
            }

            await _unitOfWork.Save();
        }
    }
}
