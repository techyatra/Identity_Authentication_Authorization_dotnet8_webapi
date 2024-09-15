using IdentityAuthenticationandAuthorization.Interfaces;
using IdentityAuthenticationandAuthorization.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityAuthenticationandAuthorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<string>> AddRoleAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<bool> AddUserRoleAsync(string email, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    rolesList.Add(role);
                }
            }
            if (user != null & rolesList.Count == roles.Length)
            {
                var result = await _userManager.AddToRolesAsync(user, rolesList);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var roles = _roleManager.Roles.Select(s => new RoleModel {
                Id = Guid.Parse(s.Id),
                Name = s.Name }).ToList();
            return roles;
        }

        public async Task<List<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
