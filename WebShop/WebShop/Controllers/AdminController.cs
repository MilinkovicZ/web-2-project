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
            var users = await _adminService.GetAllVerified();
            return Ok(users);
        }

        [HttpGet("GetUnverified")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnverified()
        {
            var users = await _adminService.GetAllUnverified();
            return Ok(users);
        }

        [HttpPost("VerifyUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> VerifyUser(UserVerifyDTO userVerifyDTO)
        {
            await _adminService.VerifyUser(userVerifyDTO);

            return Ok();
        }

        [HttpGet("GetOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _adminService.GetAllOrders();
            return Ok(orders);
        }
    }
}
