using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface ISellerInterface
    {
        Task CreateNewProduct(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
        Task<List<Product>> GetAllProduct();
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetNewOrders();
    }
}
