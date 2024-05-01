using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.User
{
    public class CreateUserDto
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
