using Core.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<User>> GetPaginatedUsersAsync(string search, int pageIndex = 1, int pageSize = 10);
        Task<User> GetUserByIdAsync(int userId);
        Task<IdentityResult> CreateUserAsync(UserModel model);
        Task<IdentityResult> UpdateUserAsync(UserModel model);
        Task<IdentityResult> DeleteUserAsync(int userId);
    }
}