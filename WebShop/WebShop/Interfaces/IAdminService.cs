using WebShop.DTO;
using WebShop.Enums;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllVerified();
        Task<List<User>> GetAllUnverified();
        Task VerifyUser(UserVerifyDTO userVerifyDTO);
        Task<List<Order>> GetAllOrders();
    }
}
