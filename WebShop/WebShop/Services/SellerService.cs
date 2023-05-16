using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class SellerService : ISellerInterface
    {
        public Task CreateNewProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetNewOrders()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
