using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int userId))
                throw new BadRequestException("Error occured with ID. Please try again.");
            var userProfile = await _userService.GetUserProfile(userId);

            return Ok(userProfile);
        }

        [HttpPut("EditProfile")]
        [Authorize]
        public async Task<IActionResult> EditUserProfile(EditUserDTO editUserDTO)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int userId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _userService.EditUserProfile(userId, editUserDTO);
            return Ok();
        }

        [HttpPut("AddPicture")]
        [Authorize]
        public async Task<IActionResult> AddPicture([FromForm] IFormFile image)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int userId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _userService.AddPicture(userId, image);
            return Ok();
        }
    }
}
