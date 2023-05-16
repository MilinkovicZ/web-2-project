using WebShop.DTO;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class SellerService : ISellerService
    {
        public Task CreateNewProduct(CreateProductDTO productDTO, int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int productId, int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDTO>> GetAllOrders(int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetAllProduct(int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDTO>> GetNewOrders(int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(int productId, ProductDTO productDTO, int sellerId)
        {
            throw new NotImplementedException();
        }
    }
}
