using WebShop.DTO;

namespace WebShop.Interfaces
{
    public interface IUserService
    {
        public Task<UserProfileDTO> GetUserProfile(int id);
        public Task EditUserProfile(int id, EditUserDTO editUserDTO);
    }
}
