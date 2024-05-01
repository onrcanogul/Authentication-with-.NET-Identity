using Business.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRoles();
        Task CreateRoleAsync(string name);
        Task DeleteRoleAsync(string id);
    }
}
