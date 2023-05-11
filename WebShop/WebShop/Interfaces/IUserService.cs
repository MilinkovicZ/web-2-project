using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);
    }
}
