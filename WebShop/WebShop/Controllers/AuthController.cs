using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            string token = await _authService.Login(userLoginDTO);
            return Ok(new { token = token });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            await _authService.Register(userRegisterDTO);
            return Ok();
        }

        [HttpPost("RegisterViaGoogle")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterViaGoogle(GoogleLoginDTO googleLoginDTO)
        {
            var token = await _authService.RegisterViaGoogle(googleLoginDTO);
            return Ok(new {token = token});
        }
    }
}