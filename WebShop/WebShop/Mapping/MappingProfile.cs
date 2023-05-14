using AutoMapper;
using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, ProfileDTO>().ReverseMap();
        }
    }
}
