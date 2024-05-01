using Business.DTOs.Token;
using Business.DTOs.User;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUserDto user);
        Task<GetAllUsersDto> GetUsersAsync();
        Task<GetUserByIdDto> GetUserByIdAsync(string id);
        Task<bool> DeleteUser(string id);
        Task AssignRoleToUser(string userId, List<string> roles);
        Task<List<string>> GetUserRole(string userId);

        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate);

        

    }
}
