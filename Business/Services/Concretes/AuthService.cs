using Business.DTOs.Token;
using Business.Services.Abstracts;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenService _tokenService;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;

        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
        }

        private async Task<AppUser> FindUser(string usernameOrEmail)
        {
            AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
                if (user == null)
                {
                    throw new Exception("User is not found");
                }
            }
            return user;
        }

        public async Task<bool> LoginCookie(string usernameOrEmail, string password)
        {
            AppUser user = await FindUser(usernameOrEmail);
            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            return result.Succeeded;
        }

        public async Task<TokenDto> LoginJwt(string usernameOrEmail, string password)
        {
            AppUser user = await FindUser(usernameOrEmail);
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password,false);
            if (result.Succeeded)
            {
                TokenDto token = _tokenService.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration);

                return new()
                {
                    AccessToken = token.AccessToken,
                    Expiration = token.Expiration,
                    RefreshToken = token.RefreshToken
                };
            }
            throw new Exception("Email or password is wrong");
        }

        public async Task Logout()
        {
           await _signInManager.SignOutAsync();
           //The responsibility for logging out for JWT is on ClientSide
        }

        public async Task<TokenDto> RefreshTokenLogin(string refreshToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if(user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                TokenDto token = _tokenService.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration);

                return new()
                {
                    AccessToken = token.AccessToken,
                    Expiration = token.Expiration,
                    RefreshToken = token.RefreshToken
                };
            }
            throw new Exception("Refresh token is invalid or user is not found");
        }
    }
}
