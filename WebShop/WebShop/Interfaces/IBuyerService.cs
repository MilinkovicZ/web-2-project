using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IBuyerService
    {
        Task<List<ProductDTO>> GetAllProducts();
        Task CreateOrder(CreateOrderDTO orderDTO, int buyerId);
        Task DeclineOrder(int orderId, int buyerId);
        Task<List<OrderDTOWithTime>> GetMyOrders(int buyerId);
        Task OrderDeliever(int orderId);
    }
}
