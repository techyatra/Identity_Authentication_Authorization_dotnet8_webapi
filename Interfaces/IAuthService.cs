using IdentityAuthenticationandAuthorization.Models;

namespace IdentityAuthenticationandAuthorization.Interfaces
{
    public interface IAuthService
    {
        Task<List<RoleModel>> GetRolesAsync();
        Task<List<string>> GetUserRolesAsync(string email);
        Task<bool> AddUserRoleAsync(string email, string[] roles);
        Task<List<string>> AddRoleAsync(string[] roles);
    }
}
