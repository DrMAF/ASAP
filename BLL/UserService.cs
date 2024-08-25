using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BLL
{

    public class UserService : IUserService
    {
        readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(UserModel model)
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

        public async Task<PaginatedResult<User>> GetUsersPaginated(string search, int pageIndex = 1, int pageSize = 10)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(search))
            {
                users = _userManager.Users.Where(usr => usr.FirstName.Contains(search)
                || usr.LastName.Contains(search)
                || (!string.IsNullOrEmpty(usr.Email) && usr.Email.Contains(search))
                || (!string.IsNullOrEmpty(usr.PhoneNumber) && usr.PhoneNumber.Contains(search)));
            }

            int count = users.Count();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            users = users.OrderBy(usr => usr.Id).Skip(pageIndex - 1).Take(pageSize);

            var result = users.ToList();

            return new PaginatedResult<User>(result, pageIndex, totalPages);
        }

        public async Task<IdentityResult> UpdateUser(UserModel model)
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

            return null;
        }

        public async Task<IdentityResult> DeleteUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {                
                var result = await _userManager.DeleteAsync(user);

                return result;
            }

            return null;
        }
    }
}
