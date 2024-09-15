using IdentityAuthenticationandAuthorization.Interfaces;
using IdentityAuthenticationandAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IdentityAuthenticationandAuthorization.Controllers
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
        [Authorize(Roles = "admin, user")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _authService.GetRolesAsync();
            return Ok(roles);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var roles = await _authService.GetUserRolesAsync(email);
            return Ok(roles);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(string[] roles)
        {
            var roleList = await _authService.AddRoleAsync(roles);
            return Ok(roleList);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("AddUserRoles")]
        public async Task<IActionResult> AddUserRoles([FromBody] AddUserRolesModel userRoleModel)
        {
            var roles = await _authService.AddUserRoleAsync(userRoleModel.Email, userRoleModel.Roles);
            return StatusCode((int)HttpStatusCode.Created, roles);
        }
    }
}
