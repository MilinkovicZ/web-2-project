using AutoMapper.Configuration.Conventions;
using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface ISellerService
    {
        Task CreateNewProduct(CreateProductDTO productDTO, int sellerId);
        Task UpdateProduct(int productId, ProductDTO productDTO, int sellerId);
        Task DeleteProduct(int productId, int sellerId);
        Task<List<ProductDTO>> GetAllProduct(int sellerId);
        Task<List<OrderDTO>> GetAllOrders(int sellerId);
        Task<List<OrderDTO>> GetNewOrders(int sellerId);
    }
}
