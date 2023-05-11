using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.DTO;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserLoginDTO userLoginDTO)
        {
            User loggedUser = await userService.GetUser(userLoginDTO.Email, userLoginDTO.Password);

            var claims = new[] 
            {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"] ?? "defaultdefault11"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", loggedUser.Id.ToString()),
                        new Claim("Email", loggedUser.Email),
                        new Claim("UserType", loggedUser.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "defaultdefault11"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], 
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}