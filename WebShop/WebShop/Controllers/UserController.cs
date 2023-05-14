using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int UserID))
                throw new BadRequestException("Error occured with ID. Please try again.");
            var userProfile = await _userService.GetUserProfile(UserID);

            return Ok(userProfile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditUserProfile(EditUserDTO editUserDTO)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int UserID))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _userService.EditUserProfile(UserID, editUserDTO);
            return Ok();
        }
    }
}
