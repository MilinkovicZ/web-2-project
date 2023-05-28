using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
        }

        private async Task<User> GetUser(string email, string password)
        {
            var users = await _unitOfWork.UsersRepository.GetAll();
            User? user = users.FirstOrDefault(u => u.Email == email);
            if (user == null)            
                throw new NotFoundException($"User with email: {email} could not be found.");
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new BadRequestException("Invalid Password");

            return user;
        }

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            User loggedUser = await GetUser(userLoginDTO.Email, userLoginDTO.Password);

            if (loggedUser.UserType == UserType.Seller)
            {
                if (loggedUser.VerificationState == VerificationState.Denied)
                    throw new BadRequestException("You are denied by administrator as a seller!");
                else if (loggedUser.VerificationState == VerificationState.Waiting)
                    throw new BadRequestException("You are still not accepted by administator. Please wait!");
            }

            var claims = new[]
            {                        
                        new Claim(ClaimTypes.Name, loggedUser.Username),
                        new Claim(ClaimTypes.Role, loggedUser.UserType.ToString()),
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
            var users = await _unitOfWork.UsersRepository.GetAll();

            var existingEmail = users.FirstOrDefault(u => u.Email == userRegisterDTO.Email);
            if (existingEmail != null)
                throw new BadRequestException("User with this email is already registered");

            var existingUsername = users.FirstOrDefault(u => u.Username == userRegisterDTO.Username);
            if (existingUsername != null)
                throw new BadRequestException("User with this username is already registered");

            if (userRegisterDTO.Password != userRegisterDTO.ConfirmPassword)
                throw new BadRequestException("Password are not matching. Please try again");

            userRegisterDTO.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            var newUser = new User
            {
                Username = userRegisterDTO.Username,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                FullName = userRegisterDTO.FullName,
                BirthDate = userRegisterDTO.BirthDate,
                Address = userRegisterDTO.Address,
                UserType = userRegisterDTO.UserType
            };

            if (newUser.UserType == UserType.Admin)
                throw new UnauthorizedException("Admin can't be registered!");
            else if (newUser.UserType == UserType.Seller)
            {
                newUser.VerificationState = VerificationState.Waiting;
                await _mailService.SendEmail("Verification", "Your account is successfully registered and is currently waiting for administrator to approve", newUser.Email);
            }
            else
                newUser.VerificationState = VerificationState.Accepted;

            await _unitOfWork.UsersRepository.Insert(newUser);
            await _unitOfWork.Save();
        }
        public Task RegisterViaGoogle(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }
    }    
}
