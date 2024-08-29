using Core.Entities;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        PaginatedResult<User> GetPaginatedUsersAsync(string search);
        Task<User> GetUserByIdAsync(int userId);
        Task<IdentityResult> CreateUserAsync(UserModel model);
        Task<IdentityResult> UpdateUserAsync(UserModel model);
        Task<IdentityResult> DeleteUserAsync(int userId);
    }
}