using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string  Email   { get; set; }
        public string Role { get; set; }
    }
}
