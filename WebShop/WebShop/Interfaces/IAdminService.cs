using WebShop.DTO;
using WebShop.Enums;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllVerified(int adminId);
        Task<List<User>> GetAllUnverified(int adminId);
        Task VerifyUser(UserVerifyDTO userVerifyDTO, int adminId);
        Task<List<OrderDTO>> GetAllOrders(int adminId);
    }
}
