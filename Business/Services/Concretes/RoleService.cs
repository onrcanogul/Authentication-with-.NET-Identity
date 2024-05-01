using AutoMapper;
using Business.DTOs.Role;
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
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task CreateRoleAsync(string name)
        {
           IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(),Name = name });
        }

        public async Task DeleteRoleAsync(string id)
        {
           IdentityResult result = await _roleManager.DeleteAsync(new() { Id = id });
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            List<AppRole> roles = await _roleManager.Roles.ToListAsync();
            List<RoleDto> rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return rolesDto;
        
        }
    }
}
