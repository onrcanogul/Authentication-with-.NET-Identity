using AutoMapper;
using Business.DTOs.Role;
using Business.DTOs.User;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configurations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateUserDto, AppUser>().ReverseMap();
            CreateMap<GetUserByIdDto, AppUser>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppRole, RoleDto>().ReverseMap();
        }
    }
}
