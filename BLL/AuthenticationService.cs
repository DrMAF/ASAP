using Core.Entities;
using Core.Interfaces.Services;
using Core.Settings;
using Core.ViewModels.Responses;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly UserManager<User> _userManager;
        readonly AuthSettings _settings;

        public AuthenticationService(UserManager<User> userService, IOptions<AuthSettings> settings)
        {
            _userManager = userService;
            _settings = settings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateUser(LoginViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthenticationResponse();
            }

            //var roles = await _userService.GetRolesAsync(user);

            var claims = new ClaimsIdentity([
                                        new Claim("Id", user.Id.ToString()),
                                        new Claim("UserName", user.UserName),
                                        new Claim("Name",  $"{user.FirstName} {user.LastName}"),
                                        new Claim("Email", user.Email)
                                        ]);

            string accessToken = await GenerateToken(_settings.TokenSecret, _settings.TokenExpirationMunites, _settings.Issuer, _settings.Audience,/*roles,*/ claims);
            string refreshToken = await GenerateToken(_settings.RefreshTokenSecret, _settings.RefreshTokenExpirationMunites, _settings.Issuer, _settings.Audience, null);

            //Add(new AuthenticationToken
            //{
            //    UserId = user.Id,
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken
            //});

            return new AuthenticationResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                //Roles = new List<string> { "admin", "editor" }
            };
        }

        public async Task<string> GenerateToken(string secret, double expirationInMinutes, string issuer, string audience, ClaimsIdentity claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.TokenSecret));

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_settings.TokenExpirationMunites),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Audience = _settings.Audience,
                Issuer = _settings.Issuer
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public UserToken GetByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }


        //public UserToken GetByRefreshToken(string refreshToken)
        //{
        //    return _tokenService.GenerateUserTokenAsync(null,).FirstOrDefault(tkn => tkn.RefreshToken == refreshToken);
        //}

        //public AuthorizationResponse AuthenticateUser(User user)
        //{
        //    Delete(tkn => tkn.UserId == user.Id);

        //    List<Claim> Claims = new List<Claim>()
        //                            {
        //                                new Claim("Id", user.Id.ToString()),
        //                                new Claim("UserName", user.UserName),
        //                                new Claim("Name",  $"{user.FirstName} {user.LastName}"),
        //                                new Claim("Email", user.Email)
        //    };

        //    string accessToken = GenerateAccessToken(_settings.Issuer, _settings.Audience, _settings.TokenSecret, _settings.TokenExpirationMunites, Claims);
        //    string refreshToken = RefreshToken();

        //    Add(new UserToken
        //    {
        //        UserId = user.Id,
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken
        //    });

        //    return new AuthorizationResponse
        //    {
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken,
        //        Roles = new List<string> {"admin", "editor" }
        //    };
        //}

        //private string GenerateAccessToken(string issuer, string audience, string tokenSecret, double expirationMin, List<Claim> Claims = null)
        //{
        //    SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
        //    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    JwtSecurityToken token = new JwtSecurityToken(
        //        issuer,
        //        audience,
        //        Claims,
        //        DateTime.UtcNow,
        //        DateTime.UtcNow.AddMinutes(expirationMin),
        //    signingCredentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private string RefreshToken()
        //{
        //    return GenerateAccessToken(_settings.Issuer, _settings.Audience, _settings.RefreshedTokenSecret, _settings.RefreshedTokenExpirationMunites, null);
        //}

        //public bool ValidateRefreshToken(string refreshToken)
        //{
        //    TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
        //    {
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.RefreshedTokenSecret)),
        //        // ValidIssuer = _config.Issuer,
        //        //  ValidAudience = _config.Audience,
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ValidateIssuerSigningKey = true,
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        jwtSecurityTokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out var securityToken);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //ex.LogError();
        //        return false;
        //    }
        //}
    }
}
