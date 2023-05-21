using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetVerified")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVerified()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int adminId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            var users = await _adminService.GetAllVerified(adminId);
            return Ok(users);
        }

        [HttpGet("GetUnverified")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnverified()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int adminId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            var users = await _adminService.GetAllUnverified(adminId);
            return Ok(users);
        }

        [HttpPost("VerifyUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> VerifyUser(UserVerifyDTO userVerifyDTO)
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int adminId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            await _adminService.VerifyUser(userVerifyDTO, adminId);

            return Ok();
        }

        [HttpGet("GetOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!int.TryParse((User.Claims.First(c => c.Type == "UserId").Value), out int adminId))
                throw new BadRequestException("Error occured with ID. Please try again.");

            var orders = await _adminService.GetAllOrders(adminId);
            return Ok(orders);
        }
    }
}
