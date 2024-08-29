using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuth(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapPost("login", Login);

            return endpoint;
        }

        private static async Task<IResult> Login(UserManager<User> userManager, IAuthenticationService authorizationService, [FromBody] LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return Results.BadRequest("Invalid username and/or password.");
            }

            var result = await authorizationService.AuthenticateUser(model);

            return Results.Ok(result);
        }
    }
}
