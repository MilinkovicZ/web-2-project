using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<User> GetUser(string email, string password)
        {
            var users = await unitOfWork.UsersRepository.GetAllAsync();
            User? user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if(user == null)
            {
                throw new NotFoundException($"User with email: {email} and password: {password} could not be found.");
            }
            return user;
        }
    }    
}
