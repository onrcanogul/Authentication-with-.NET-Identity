using AutoMapper;
using Business.DTOs.User;
using Business.Services.Abstracts;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AssignRoleToUser(string userId, List<string> roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
              await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
              await _userManager.AddToRolesAsync(user, roles);
                //..
                //..
            }
        }

        public async Task<bool> CreateUserAsync(CreateUserDto user)
        {
           IdentityResult result =  await _userManager.CreateAsync(_mapper.Map<AppUser>(user),user.Password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<GetUserByIdDto> GetUserByIdAsync(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            
            GetUserByIdDto userDto = _mapper.Map<GetUserByIdDto>(user);
            userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            return userDto;
        }

        public async Task<GetAllUsersDto> GetUsersAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UserDto> usersDto = _mapper.Map<List<UserDto>>(users);
            return new GetAllUsersDto()
            {
                TotalCount = users.Count,
                Users = usersDto
            };
        }
        public async Task<List<string>> GetUserRole(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenEndDate.AddMinutes(1);
                await _userManager.UpdateAsync(user);
            }
            else throw new Exception("User is not found");
        }
    }
}
