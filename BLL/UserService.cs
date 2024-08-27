using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class UserService : IUserService
    {
        readonly UserManager<User> _userManager;
        readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public PaginatedResult<User> GetPaginatedUsersAsync(string search)
        {
            try
            {
                var users = _userManager.Users;

                if (!string.IsNullOrEmpty(search))
                {
                    users = _userManager.Users.Where(usr => usr.FirstName.Contains(search)
                    || usr.LastName.Contains(search)
                    || (!string.IsNullOrEmpty(usr.Email) && usr.Email.Contains(search))
                    || (!string.IsNullOrEmpty(usr.PhoneNumber) && usr.PhoneNumber.Contains(search)));
                }

                //int count = users.Count();
                //int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                users = users.OrderBy(usr => usr.Id);//.Skip(pageIndex - 1).Take(pageSize);

                var result = users.ToList();

                return new PaginatedResult<User>(result, 1, 10);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in GetPaginatedUsersAsync: {ex}");

                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return null;
                }

                User user = await _userManager.FindByIdAsync(userId.ToString());

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in GetUserByIdAsync: {ex}");

                return null;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(UserModel model)
        {
            try
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.Email
                };

                var res = await _userManager.CreateAsync(user);

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in CreateUserAsync: {ex}");

                return null;
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(UserModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in UpdateUserAsync: {ex}");
            }

            return null;
        }

        public async Task<IdentityResult> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in DeleteUserAsync: {ex}");
            }

            return null;
        }
    }
}
