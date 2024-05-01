using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.User
{
    public class GetAllUsersDto
    {
        public List<UserDto> Users { get; set; }
        public int TotalCount { get; set; }
        
    }
}
