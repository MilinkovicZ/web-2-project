using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        private async Task<User> GetUser(string email, string password)
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            User? user = users.FirstOrDefault(u => u.Email == email);
            if (user == null || user.IsDeleted)            
                throw new NotFoundException($"User with email: {email} could not be found.");
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new BadRequestException("Invalid Password");

            return user;
        }

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            User loggedUser = await GetUser(userLoginDTO.Email, userLoginDTO.Password);
            
            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? "defaultdefault11"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", loggedUser.Id.ToString()),
                        new Claim("Email", loggedUser.Email),
                        new Claim("UserType", loggedUser.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "defaultdefault11"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task Register(UserRegisterDTO userRegisterDTO)
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            User? existingUser = users.FirstOrDefault(u => u.Email == userRegisterDTO.Email);
            if (existingUser != null)
                throw new BadRequestException("User with this email is already registered");

            userRegisterDTO.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            var newUser = _mapper.Map<User>(userRegisterDTO);
            await _unitOfWork.UsersRepository.Insert(newUser);
            await _unitOfWork.Save();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }    
}
