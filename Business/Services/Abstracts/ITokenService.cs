using Business.DTOs.Token;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ITokenService
    {
        TokenDto CreateAccessToken(AppUser user);
        string CreateRefreshToken();
    }
}
