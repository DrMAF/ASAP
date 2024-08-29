using Core.Entities;
using Core.ViewModels.Responses;
using Core.ViewModels.Users;

namespace Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        UserToken GetByRefreshToken(string refreshToken);
        Task<AuthenticationResponse> AuthenticateUser(LoginViewModel model);
        bool ValidateRefreshToken(string refreshToken);
    }
}
