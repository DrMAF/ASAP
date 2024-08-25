using Core.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<User>> GetUsersPaginated(string search, int pageIndex = 1, int pageSize = 10);
        Task<IdentityResult> CreateUser(UserModel model);
        Task<IdentityResult> UpdateUser(UserModel model);
        Task<IdentityResult> DeleteUser(int userId);
    }
}