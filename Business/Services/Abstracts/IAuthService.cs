using Business.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IAuthService
    {
        Task<bool> LoginCookie(string username, string password);

        Task<TokenDto> LoginJwt(string username, string password);

        Task<TokenDto> RefreshTokenLogin(string refreshToken);

        Task Logout();
    }
}
