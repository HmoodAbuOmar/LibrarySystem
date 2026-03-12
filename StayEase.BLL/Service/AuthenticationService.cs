using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StayEase.DAL.DTO.Request;
using StayEase.DAL.DTO.Response;
using StayEase.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StayEase.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid Email"
                    };
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!isPasswordValid)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid password"
                    };
                }

                return new LoginResponse
                {
                    Success = true,
                    Message = "Login Successfully",
                    AccessToken = await GenerateAccessToken(user)
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "An Unexepted errors",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var user = request.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = "User Creation Faield",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                await _userManager.AddToRoleAsync(user, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);

                var emailUrl = $"https://localhost:7142/api/Identity/Account/ConfirmEmail?token={token}&userid={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "Welcome to KASHOP", $"<h1> Welcome .. {user.UserName}<h1> " +
                    $"<a href='{emailUrl}'> Confirm Email <a/>");

                return new RegisterResponse
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "An Unexepted errors",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<bool> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
