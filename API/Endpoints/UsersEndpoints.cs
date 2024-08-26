using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapAccount(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("", GetUsers);
            endpoint.MapGet("getById/{id}", GetUserById);
            endpoint.MapPost("", AddUser);
            endpoint.MapPut("", UpdateUser);
            endpoint.MapDelete("", DeleteUser);

            return endpoint;



        }

        private static async Task<IResult> GetUsers(IUserService userService, string search = "", int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 1 : pageSize;

            var users = await userService.GetPaginatedUsersAsync(search, pageIndex, pageSize);

            return Results.Ok(users);
        }

        private static async Task<IResult> GetUserById(int userId, IUserService userService)
        {
            User user = await userService.GetUserByIdAsync(userId);

            return Results.Ok(user);
        }

        private static async Task<IResult> AddUser(IUserService userService, [FromBody] UserModel model)
        {
            IdentityResult res = await userService.CreateUserAsync(model);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }


        private static async Task<IResult> UpdateUser(IUserService userService, [FromBody] UserModel model)
        {
            if (model.Id <= 0)
            {
                return Results.BadRequest("Id required");
            }

            var res = await userService.UpdateUserAsync(model);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }

        private static async Task<IResult> DeleteUser(IUserService userService, int userId)
        {
            if (userId <= 0)
            {
                return Results.BadRequest("Id required");
            }

            var res = await userService.DeleteUserAsync(userId);

            if (res.Succeeded)
            {
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(res);
            }
        }
    }
}
